using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RushNDestroy
{
    public class GameManager : MonoBehaviour
    {
        public GameObject entityPrefab;
        private List<EntityEvents> playerUnits, enemyUnits;
        private List<EntityEvents> playerStructures, enemyStructures;
        private List<EntityEvents> allPlayers, allEnemies;
        private List<EntityEvents> allEntities;
        public EntityData entityData;
        private CardManager cardManager;
        public ManaRefill mana;

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
        public void SpawnEntity(EntityData entity, Vector2 position, EntityEnums.Faction pFaction)
        {
            //Prefab to spawn is the associatedPrefab if it's the Player faction, otherwise it's alternatePrefab. But if alternatePrefab is null, then first one is taken
            GameObject prefabToSpawn = (pFaction == EntityEnums.Faction.Player) ? entity.playerPrefab : ((entity.enemyPrefab == null) ? entity.playerPrefab : entity.enemyPrefab);
            GameObject character = Instantiate<GameObject>(entity.playerPrefab, position, Quaternion.identity);
            SetupEntity(character, entity, pFaction);
            mana.mana -= entity.cost;
            //updateAllPlaceables = true;
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
            }
        }
        private void AddEntityToList(EntityEvents entity)
        {
            allEntities.Add(entity);
            if (entity.faction == EntityEnums.Faction.Player)
            {
                allPlayers.Add(entity);
                if (entity.type == EntityEnums.Type.Unit)
                    playerUnits.Add(entity);
                else
                    playerStructures.Add(entity);
            }
            else if (entity.faction == EntityEnums.Faction.Enemy)
            {
                allEnemies.Add(entity);
                if (entity.type == EntityEnums.Type.Unit)
                    enemyUnits.Add(entity);
                else
                    playerStructures.Add(entity);
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
                if (entity.type == EntityEnums.Type.Unit)
                    playerUnits.Remove(entity);
                else
                    playerStructures.Remove(entity);
            }
            else if (entity.faction == EntityEnums.Faction.Enemy)
            {
                allEnemies.Remove(entity);
                if (entity.type == EntityEnums.Type.Unit)
                    enemyUnits.Remove(entity);
                else
                    playerStructures.Remove(entity);
            }
            else
                Debug.LogError("Error in removing a Placeable from one of the player/opponent lists");
        }
    }
}