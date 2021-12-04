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
        private AISpawning aiSpawning;
        public ManaRefill mana;
        public GameOverMenu gameOverMenu;
        [HideInInspector] public bool gameOver = false;
        private int gameWon = 0;

        private void Awake()
        {
            gameOverMenu.gameObject.SetActive(false);
            aiSpawning = GetComponent<AISpawning>();
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
            aiSpawning.SpawnAIUnit += SpawnEntity;
        }
        private void Start()
        {
            Time.timeScale = 1;
            SetupEntity(enemyCastle, castleData, EntityEnums.Faction.Enemy);
            SetupEntity(playerCastle, castleData, EntityEnums.Faction.Player);
            for (int i = 0; i < playerTowers.Length; i++)
                SetupEntity(playerTowers[i], towerData, EntityEnums.Faction.Player);
            for (int i = 0; i < enemyTowers.Length; i++)
                SetupEntity(enemyTowers[i], towerData, EntityEnums.Faction.Enemy);
        }
        private void Update()
        {
            EntityEvents targetToPass; //ref
            EntityEvents p; //ref

            for (int pN = 0; pN < allEntities.Count; pN++)
            {
                p = allEntities[pN];

                switch (p.state)
                {
                    case EntityEvents.States.Idle:
                        if (p.targetType == EntityEnums.TargetType.None)
                            break;

                        bool foundTarget = FindClosestInList(p.transform.position, GetAttackList(p.faction, p.targetType), out targetToPass);
                        if (!targetToPass)
                        {
                            gameOver = true;
                            GameOver();
                        } //this should only happen on Game Over
                        else
                        {
                            p.SetTarget(targetToPass);
                            p.Seek();
                        }
                        break;
                    case EntityEvents.States.Seeking:
                        if (p.entityType == EntityEnums.Type.Structure || p.entityType == EntityEnums.Type.Castle)
                        {
                            bool targetFound = FindClosestInList(p.transform.position, GetAttackList(p.faction, p.targetType), out targetToPass);
                            if (!targetToPass)
                            {
                                Debug.Log("No enemy left");
                            }
                        } //this should only happen on Game Over
                        else if (p.TargetInRange())
                            p.StartFighting();
                        break;

                    case EntityEvents.States.Fighting:
                        if (p.TargetInRange())
                            if (Time.time >= p.lastAttackTime + p.attackRatio)
                            {
                                p.DoDamage();
                            }
                        break;

                    case EntityEvents.States.Dead:
                        if (p.TargetInRange() == false) Debug.Log(p.name + " | is dead");
                        p.gameObject.SetActive(false);
                        RemoveEntityFromList(p);
                        Debug.Log("He's dead, he shouldn't be here");
                        break;
                    default:
                        Debug.Log("This entity has no state!");
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
            if (pFaction == EntityEnums.Faction.Player)
                mana.mana -= entity.cost;
        }
        private void SetupEntity(GameObject gameObject, EntityData entity, EntityEnums.Faction faction)
        {
            switch (entity.entityType)
            {
                case EntityEnums.Type.Unit:
                    UnitData unitData = gameObject.GetComponent<UnitData>();
                    unitData.Activate(faction, entity);
                    unitData.OnDoDamage += OnEntityDidDamge;
                    AddEntityToList(unitData);
                    break;
                case EntityEnums.Type.Structure:
                    BuildingData bd = gameObject.GetComponent<BuildingData>();
                    bd.Activate(faction, entity);
                    bd.OnDoDamage += OnEntityDidDamge;
                    AddEntityToList(bd);
                    break;
                case EntityEnums.Type.Castle:
                    BuildingData buildingData = gameObject.GetComponent<BuildingData>();
                    buildingData.Activate(faction, entity);
                    buildingData.OnDoDamage += OnEntityDidDamge;
                    AddEntityToList(buildingData);
                    //special case for castles
                    if (entity.entityType == EntityEnums.Type.Castle)
                    {
                        buildingData.OnDie += OnCastleDead;
                    }
                    //To implement later, will rebuild NavMesh after a tower is destroyed so area is walkable
                    //navMesh.BuildNavMesh();
                    break;
            }
            gameObject.GetComponent<EntityEnums>().OnDie += EntityDead;
        }

        private void OnEntityDidDamge(EntityEvents e)
        {
            if (e.target.state != EntityEvents.States.Dead)
            {
                float newHp = e.target.SufferDamage(e.damage);
                e.target.healthBar.SetHealth(newHp);
            }
        }

        private void OnCastleDead(EntityEnums castle)
        {
            castle.OnDie -= OnCastleDead;
            if (castle.faction == EntityEnums.Faction.Enemy)
                gameWon = 2;
            else
                gameWon = 1;
            gameOver = true;
            GameOver();
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
            switch (t)
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
            bool foundTarget = false;
            float closestDistanceSqr = Mathf.Infinity;

            for (int i = 0; i < list.Count; i++)
            {
                float sqrDistance = (p - (Vector2)list[i].transform.position).sqrMagnitude;
                if (sqrDistance < closestDistanceSqr)
                {
                    t = list[i];
                    closestDistanceSqr = sqrDistance;
                    foundTarget = true;
                }
            }
            return foundTarget;
        }

        private void EntityDead(EntityEnums entity)
        {
            entity.OnDie -= EntityDead; //Remove the listener

            switch (entity.entityType)
            {
                case EntityEnums.Type.Unit:
                    UnitData ud = (UnitData)entity;
                    RemoveEntityFromList(ud);
                    ud.OnDoDamage -= OnEntityDidDamge;
                    StartCoroutine(Delete(ud));
                    break;

                case EntityEnums.Type.Structure:
                    BuildingData tower = (BuildingData)entity;
                    RemoveEntityFromList(tower);
                    tower.OnDoDamage -= OnEntityDidDamge;
                    StartCoroutine(Delete(tower));
                    break;

                case EntityEnums.Type.Castle:
                    BuildingData castle = (BuildingData)entity;
                    RemoveEntityFromList(castle);
                    castle.OnDoDamage -= OnEntityDidDamge;
                    StartCoroutine(Delete(castle));
                    break;
            }
        }
        private IEnumerator Delete(EntityEvents e)
        {
            yield return new WaitForSeconds(1f);

            Destroy(e.gameObject);
        }

        public void GameOver()
        {
            if (gameOver)
            {
                Time.timeScale = 0;

                if (gameWon == 2) //game was won
                {
                    gameOverMenu.coinsAmount.text = "+" + 10;
                    gameOverMenu.trophiesAmount.text = "+" + 10;
                    gameOverMenu.rewardsMenu.SetActive(true);
                    gameOverMenu.tieText.SetActive(false);
                    gameOverMenu.gameObject.SetActive(true);
                }
                else if (gameWon == 1) //game was lost
                {
                    gameOverMenu.coinsAmount.text = "-" + 10;
                    gameOverMenu.trophiesAmount.text = "-" + 10;
                    gameOverMenu.rewardsMenu.SetActive(true);
                    gameOverMenu.tieText.SetActive(false);
                    gameOverMenu.gameObject.SetActive(true);
                }
                else //neither side won
                {
                    gameOverMenu.rewardsMenu.SetActive(false);
                    gameOverMenu.tieText.SetActive(true);
                    gameOverMenu.gameObject.SetActive(true);
                }
            }
        }

    }
}
