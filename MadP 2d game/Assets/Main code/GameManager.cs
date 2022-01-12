using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace RushNDestroy
{
    public class GameManager : MonoBehaviour
    {
        [Header("Structures, rewards and resouces data")]
        public GameObject playerCastle, enemyCastle;
        public EntityData castleData;
        public GameObject[] playerTowers;
        public GameObject[] enemyTowers;
        public EntityData towerData;
        public RewardsData rewardsData;
        public ManaRefill mana;
        public GameObject smokeParticles;
        public float detectRange = 1.5f;

        private List<EntityEvents> playerUnits, enemyUnits;
        private List<EntityEvents> playerStructures, enemyStructures;
        private List<EntityEvents> allPlayers, allEnemies;
        private List<EntityEvents> allEntities;

        private CardManager cardManager;
        private AISpawning aiSpawning;
        private SaveManager saveManager;

        [Header("UI components")]
        public Timer timer;
        public GameOverMenu gameOverMenu;
        public GameObject alerts;
        private Text aletrsText;
        [HideInInspector] public bool gameOver = false;
        private int gameWon = 0;
        private int killCount = 0;
        private int coins;
        private int trophies;

        private void Awake()
        {
            saveManager = GetComponent<SaveManager>();
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
            alerts.gameObject.SetActive(false);
            aletrsText = alerts.GetComponentInChildren<Text>();
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
            EntityEvents primaryTarget;//ref
            EntityEvents p; //ref

            for (int pN = 0; pN < allEntities.Count; pN++)
            {
                p = allEntities[pN];

                switch (p.state)
                {
                    case EntityEvents.States.Idle:
                        if (p.targetType == EntityEnums.TargetType.None)
                            break;

                        bool primaryTargetFound = FindClosestInList(p.targetAirborneEntities, p.transform.position, PrimaryTargetList(p.faction, p.targetType), out primaryTarget);

                        bool towerTargets = FindClosestInList(p.targetAirborneEntities, p.transform.position, UnitTargetList(p.faction, p.targetType), out targetToPass);

                        if (!primaryTarget)
                        {
                            gameOver = true;
                            GameOver();
                        } //this should only happen on Game Over
                        if (primaryTarget && p.entityType == EntityEnums.Type.Unit)
                        {
                            p.SetTarget(primaryTarget);
                            p.SeekTower();
                        }
                        else if (p.entityType == EntityEnums.Type.Structure || p.entityType == EntityEnums.Type.Castle)
                        {
                            if (targetToPass != null)
                            {
                                p.SetTarget(targetToPass);
                                p.SeekUnit();
                            }
                        }
                        else Debug.Log("Unassigned entity type for " + p.name);
                        break;

                    case EntityEvents.States.SeekingTower:

                        bool closerTargetFound = FindClosestInList(p.targetAirborneEntities, p.transform.position, AllTargetList(p.faction, p.targetType), out targetToPass);
                        if (p.TargetInRange())
                        {
                            p.StartFighting();
                        }
                        else if (Vector2.Distance(p.transform.position, targetToPass.transform.position) < detectRange)
                        {

                            p.SetTarget(targetToPass);
                            p.SeekUnit();
                        }
                        break;

                    case EntityEvents.States.SeekingUnit:
                        if (p.entityType == EntityEnums.Type.Structure || p.entityType == EntityEnums.Type.Castle)
                        {
                            bool targetFound = FindClosestInList(p.targetAirborneEntities, p.transform.position, PrimaryTargetList(p.faction, p.targetType), out targetToPass);
                            if (!targetToPass)
                            {
                                gameOver = true;
                                GameOver();
                            }
                        } //this should only happen on Game Over

                        if (p.TargetInRange())
                        {
                            p.StartFighting();
                        }
                        break;

                    case EntityEvents.States.Fighting:
                        if (p.TargetInRange())
                            if (Time.time >= p.lastAttackTime + p.attackRatio)
                            {
                                p.DoDamage();
                            }
                        break;

                    case EntityEvents.States.Dead:
                        //if (p.TargetInRange() == false) Debug.Log(p.name + " | is dead");
                        p.gameObject.SetActive(false);
                        RemoveEntityFromList(p);
                        //Debug.Log("He's dead, he shouldn't be here");
                        break;
                    default:
                        Debug.Log("This entity has no state!");
                        break;
                }
            }
            // Event alerts
            if (timer.timeRemaining <= 60f)
            {
                aletrsText.text = "Mana generation increased!";
                alerts.gameObject.SetActive(true);
            }
            if (timer.timeRemaining <= 55f)
            {
                alerts.gameObject.SetActive(false);
            }
            if (timer.timeRemaining <= 30f)
            {
                aletrsText.text = "Sudden death!";
                alerts.gameObject.SetActive(true);
            }
            if (timer.timeRemaining <= 25f)
            {
                alerts.gameObject.SetActive(false);
            }
        }

        public void SpawnEntity(EntityData entity, Vector2 position, EntityEnums.Faction pFaction)
        {
            if (entity.extraType == EntityEnums.ExtraType.Airborne)
            {
                Vector3 spawnPosition = new Vector3(position.x, position.y, -2);
                //Prefab to spawn is the associatedPrefab if it's the Player faction, otherwise it's alternatePrefab. But if alternatePrefab is null, then first one is taken
                GameObject prefabToSpawn = (pFaction == EntityEnums.Faction.Player) ? entity.playerPrefab : ((entity.enemyPrefab == null) ? entity.playerPrefab : entity.enemyPrefab);
                GameObject character = Instantiate<GameObject>(prefabToSpawn, spawnPosition, Quaternion.identity);
                SetupEntity(character, entity, pFaction);
                CreateSmoke(spawnPosition);
            }
            else
            {
                //Prefab to spawn is the associatedPrefab if it's the Player faction, otherwise it's alternatePrefab. But if alternatePrefab is null, then first one is taken
                GameObject prefabToSpawn = (pFaction == EntityEnums.Faction.Player) ? entity.playerPrefab : ((entity.enemyPrefab == null) ? entity.playerPrefab : entity.enemyPrefab);
                GameObject character = Instantiate<GameObject>(prefabToSpawn, position, Quaternion.identity);
                SetupEntity(character, entity, pFaction);
                CreateSmoke(position);
            }
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
                    bd.CreateSmokeOnDeath += CreateSmoke;

                    break;
                case EntityEnums.Type.Castle:
                    BuildingData buildingData = gameObject.GetComponent<BuildingData>();
                    buildingData.Activate(faction, entity);
                    buildingData.OnDoDamage += OnEntityDidDamge;
                    AddEntityToList(buildingData);
                    //special case for castles
                    buildingData.OnDie += OnCastleDead;
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
                {
                    playerStructures.Add(entity);
                }
            }
            else if (entity.faction == EntityEnums.Faction.Enemy)
            {
                allEnemies.Add(entity);

                if (entity.entityType == EntityEnums.Type.Unit)
                    enemyUnits.Add(entity);
                else
                {
                    enemyStructures.Add(entity);
                }
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

        private List<EntityEvents> AllTargetList(EntityEnums.Faction f, EntityEnums.TargetType t)
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

        private List<EntityEvents> PrimaryTargetList(EntityEnums.Faction f, EntityEnums.TargetType t)
        {
            switch (t)
            {
                case EntityEnums.TargetType.All:
                    return (f == EntityEnums.Faction.Player) ? enemyStructures : playerStructures;
                case EntityEnums.TargetType.OnlyBuildings:
                    return (f == EntityEnums.Faction.Player) ? enemyStructures : playerStructures;
                default:
                    Debug.LogError("NOR PLAYER NOR OPPONENT");
                    return null;
            }
        }

        private List<EntityEvents> UnitTargetList(EntityEnums.Faction f, EntityEnums.TargetType t)
        {
            switch (t)
            {
                case EntityEnums.TargetType.All:
                    return (f == EntityEnums.Faction.Player) ? enemyUnits : playerUnits;
                case EntityEnums.TargetType.OnlyBuildings:
                    return (f == EntityEnums.Faction.Player) ? enemyStructures : playerStructures;
                default:
                    Debug.LogError("NOR PLAYER NOR OPPONENT");
                    return null;
            }
        }
        private bool FindClosestInList(bool targetAirborneEntities, Vector2 p, List<EntityEvents> list, out EntityEvents t)
        {
            t = null;
            bool foundTarget = false;
            float closestDistanceSqr = Mathf.Infinity;



            for (int i = 0; i < list.Count; i++)
            {
                if (targetAirborneEntities)
                {
                    float sqrDistance = (p - (Vector2)list[i].transform.position).sqrMagnitude;
                    if (sqrDistance < closestDistanceSqr)
                    {
                        t = list[i];
                        closestDistanceSqr = sqrDistance;
                        foundTarget = true;
                    }
                }
                else
                {
                    float sqrDistance = (p - (Vector2)list[i].transform.position).sqrMagnitude;
                    if (sqrDistance < closestDistanceSqr)
                    {
                        if (list[i].extraType != EntityEnums.ExtraType.Airborne)
                        {
                            t = list[i];
                            closestDistanceSqr = sqrDistance;
                            foundTarget = true;
                        }
                    }
                }
            }
            return foundTarget;
        }

        private void EntityDead(EntityEnums entity)
        {
            entity.OnDie -= EntityDead; //Remove the listener
            if (entity.entityType == EntityEnums.Type.Unit && entity.faction == EntityEnums.Faction.Enemy)
                killCount++;
            else if (entity.entityType == EntityEnums.Type.Structure && entity.faction == EntityEnums.Faction.Enemy && timer.timeRemaining < 30)
            {
                gameWon = 2;
                gameOver = true;
                GameOver();
            }
            else if (entity.entityType == EntityEnums.Type.Structure && entity.faction == EntityEnums.Faction.Player && timer.timeRemaining < 30)
            {
                gameWon = 1;
                gameOver = true;
                GameOver();
            }

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
                    break;

                case EntityEnums.Type.Castle:
                    BuildingData castle = (BuildingData)entity;
                    RemoveEntityFromList(castle);
                    castle.OnDoDamage -= OnEntityDidDamge;
                    break;
            }
        }
        private IEnumerator Delete(EntityEvents e)
        {
            yield return new WaitForSeconds(1f);

            Destroy(e.gameObject);
        }
        private void CreateSmoke(Vector2 position)
        {
            Vector3 pos = new Vector3(position.x, position.y, -4);
            GameObject smoke = Instantiate<GameObject>(smokeParticles, pos, Quaternion.identity);
            StartCoroutine(DeleteSmoke(smoke));
        }
        private IEnumerator DeleteSmoke(GameObject smoke)
        {
            yield return new WaitForSeconds(2f);

            Destroy(smoke);
        }

        public void GameOver()
        {
            if (gameOver)
            {
                Time.timeScale = 0;

                if (gameWon == 2) //game was won
                {
                    trophies = (int)timer.timeRemaining + killCount / 10;
                    coins = (int)timer.timeRemaining + killCount / 3;
                    rewardsData.coins += coins;
                    rewardsData.trophies += trophies;

                    gameOverMenu.coinsAmount.text = "+" + coins;
                    gameOverMenu.trophiesAmount.text = "+" + trophies;
                    gameOverMenu.rewardsMenu.SetActive(true);
                    gameOverMenu.tieText.SetActive(false);
                    gameOverMenu.gameObject.SetActive(true);
                }
                else if (gameWon == 1) //game was lost
                {
                    if ((int)timer.timeRemaining > 20)
                    {
                        trophies = (int)timer.timeRemaining / 10;
                    }
                    else trophies = (int)timer.timeRemaining;

                    rewardsData.trophies -= trophies;
                    if (rewardsData.trophies < 0)
                        rewardsData.trophies = 0;

                    gameOverMenu.coinsAmount.text = "0";
                    gameOverMenu.trophiesAmount.text = "-" + trophies;
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
                if (rewardsData.trophies > 9999) rewardsData.trophies = 9999;
                if (rewardsData.coins > 9999999) rewardsData.coins = 9999999;
                saveManager.Save();
                enabled = false;
            }
        }

    }
}
