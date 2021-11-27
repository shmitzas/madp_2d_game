using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RushNDestroy
{
    public class GameManager : MonoBehaviour
    {
        
        public GameObject playerCastle, enemyCastle;
        public EntityData castleData;
        public GameObject[] playerTowers;
        public GameObject[] enemyTowers;
        public EntityData towerData;


        private List<EntityEvents> playerUnits, enemyUnits;
        private List<EntityEvents> playerStructures, enemyStructures;
        private List<EntityEvents> allPlayers, allEnemies;
        private List<EntityEvents> allEntities;
        
        private CardManager cardManager;
        public ManaRefill mana;
        private bool updateAllPlaceables = false;
        private bool gameOver = false;

        private void Awake()
        {
            cardManager = GetComponent<CardManager>();
            playerUnits = new List<EntityEvents>();
            playerStructures = new List<EntityEvents>();
            enemyUnits = new List<EntityEvents>();
            enemyStructures = new List<EntityEvents>();
            allEntities = new List<EntityEvents>();
            allPlayers = new List<EntityEvents>();
            allEnemies = new List<EntityEvents>();
            

            //Setup all necessary listeners
            cardManager.OnCardUsed += SpawnEntity;
        }
        private void Start()
        {
            SetupEntity(enemyCastle, castleData, EntityEnums.Faction.Enemy);
            SetupEntity(playerCastle, castleData, EntityEnums.Faction.Player);
            for(int i=0; i<playerTowers.Length; i++)
                SetupEntity(playerTowers[i], towerData, EntityEnums.Faction.Player);
            for(int i=0; i<enemyTowers.Length; i++)
                SetupEntity(enemyTowers[i], towerData, EntityEnums.Faction.Enemy);
        }
        private void Update()
        {

            
            if(gameOver)
                return;
            
            EntityEvents targetToPass; //ref
            EntityEvents p; //ref

            for(int pN = 0; pN < allEntities.Count; pN++)
            {
                p = allEntities[pN];

                if(updateAllPlaceables)
                 p.state = EntityEvents.States.Idle;
            switch(p.state)
            {
                case EntityEvents.States.Idle:
                         if(p.targetType == EntityEnums.TargetType.None) 
                        break;
                    
                    bool foundTarget = FindClosestInList(p.transform.position, GetAttackList(p.faction, p.targetType), out targetToPass);
                    if(foundTarget == true && targetToPass.healthRemaining > 0)
                    p.SetTarget(targetToPass);
                    p.Seek();
                    break;
                case EntityEvents.States.Seeking:
                    // if(p.TargetInRange())
                    // {
                    //     //p.Attack();
                    // }
                break;

                case EntityEvents.States.Fighting:
                // if(p.TargetInRange())
                // {

                // }
                break;

                case EntityEvents.States.Dead:
                    p.gameObject.SetActive(false);
                    RemoveEntityFromList(p);
                Debug.Log("He's dead, he shouldn't be here");
                break;
            }
            }
        }

        public void SpawnEntity(EntityData entity, Vector2 position, EntityEnums.Faction pFaction)
        {
            //Prefab to spawn is the associatedPrefab if it's the Player faction, otherwise it's alternatePrefab. But if alternatePrefab is null, then first one is taken
            GameObject prefabToSpawn = (pFaction == EntityEnums.Faction.Player) ? entity.playerPrefab : ((entity.enemyPrefab == null) ? entity.playerPrefab : entity.enemyPrefab);
            GameObject character = Instantiate<GameObject>(entity.playerPrefab, position, Quaternion.identity);
            SetupEntity(character, entity, pFaction);
            mana.mana -= entity.cost;
            updateAllPlaceables = true;
        }
        private void SetupEntity(GameObject gameObject, EntityData entity, EntityEnums.Faction faction)
        {
            switch (entity.entityType)  
            {
                case EntityEnums.Type.Unit:
                    UnitData unitData = gameObject.GetComponent<UnitData>(); 
                    unitData.Activate(faction, entity);
                    AddEntityToList(unitData);
                    HealthBar healthBar = gameObject.GetComponentInChildren<HealthBar>(); 
                    healthBar.StartHealthBar(entity.health);
                    break;
                case EntityEnums.Type.Structure:
                    BuildingData bd = gameObject.GetComponent<BuildingData>();
                    bd.Activate(faction, entity);
                    AddEntityToList(bd);
                    HealthBar hb = gameObject.GetComponentInChildren<HealthBar>();
                    hb.StartHealthBar(entity.health);
                    break;
                case EntityEnums.Type.Castle:
                    BuildingData buildingData = gameObject.GetComponent<BuildingData>();
                    buildingData.Activate(faction, entity);
                    AddEntityToList(buildingData);
                    HealthBar chb = gameObject.GetComponentInChildren<HealthBar>();
                    chb.StartHealthBar(entity.health);
                                            //special case for castles
                        if(entity.entityType == EntityEnums.Type.Castle)
                        {
                            buildingData.OnDie += OnCastleDead;
                        }
                        //To implement later, will rebuild NavMesh after a tower is destroyed so area is walkable
                        //navMesh.BuildNavMesh();
                        break;
            }
        }
        
        private void OnCastleDead(EntityEnums castle)
        {
            castle.OnDie -= OnCastleDead;
            gameOver = true;
        }
        
        private void AddEntityToList(EntityEvents entity)
        {
            allEntities.Add(entity);

            if (entity.faction == EntityEnums.Faction.Player)
            {
                allPlayers.Add(entity);

                if (entity.entityType == EntityEnums.Type.Unit)
                    playerUnits.Add(entity);
                else
                    playerStructures.Add(entity);
            }
            else if (entity.faction == EntityEnums.Faction.Enemy)
            {
                allEnemies.Add(entity);

                if (entity.entityType == EntityEnums.Type.Unit)
                    enemyUnits.Add(entity);
                else
                    enemyStructures.Add(entity);
            }
            else
                Debug.LogError("Error in adding a Placeable in one of the player/opponent lists");
        }
        private void RemoveEntityFromList(EntityEvents entity)
        {
            allEntities.Remove(entity);
            if (entity.faction == EntityEnums.Faction.Player)
            {
                allPlayers.Remove(entity);

                if (entity.entityType == EntityEnums.Type.Unit)
                    playerUnits.Remove(entity);
                else
                    playerStructures.Remove(entity);
            }
            else if (entity.faction == EntityEnums.Faction.Enemy)
            {
                allEnemies.Remove(entity);

                if (entity.entityType == EntityEnums.Type.Unit)
                    enemyUnits.Remove(entity);
                else
                    enemyStructures.Remove(entity);
            }
            else
                Debug.LogError("Error in removing a Placeable from one of the player/opponent lists");
        }

        private List<EntityEvents> GetAttackList(EntityEnums.Faction f, EntityEnums.TargetType t)
        {
            switch(t)
            {
                case EntityEnums.TargetType.All:
                    return (f == EntityEnums.Faction.Player) ? allEnemies : allPlayers;
                case EntityEnums.TargetType.OnlyBuildings:
                    return (f == EntityEnums.Faction.Player) ? enemyStructures : playerStructures;
                default:
                    Debug.LogError("NOR PLAYER NOR OPPONENT");
                    return null;
            }
        }

        private bool FindClosestInList(Vector2 p, List<EntityEvents> list, out EntityEvents t)
        {
            t = null;
            //bool foundTarget = false;
            float closestDistanceSqr = Mathf.Infinity;

            for(int i = 0; i < list.Count; i++){
                float sqrDistance = (p - (Vector2)list[i].transform.position).sqrMagnitude;
                if(sqrDistance < closestDistanceSqr)
                {
                    t = list[i];
                    closestDistanceSqr = sqrDistance;
                    //foundTarget = true;
                }
            }
            //return foundTarget;
            return list.Count != 0;
        }
    
    }
}