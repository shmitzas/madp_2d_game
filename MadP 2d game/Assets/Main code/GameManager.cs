using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RushNDestroy
{
    public class GameManager : MonoBehaviour
    {
        public GameObject entityPrefab;
        private List<EntityEvents> playerUnits, aiUnits;
        private List<EntityEvents> playerStructures, aiStructures;
        private List<EntityEvents> allEntities;
        public EntityData entityData;
        private CardManager cardManager;
        public ManaRefill mana;

        private void Awake()
        {
            cardManager = GetComponent<CardManager>();
            playerUnits = new List<EntityEvents>();
            playerStructures = new List<EntityEvents>();
            allEntities = new List<EntityEvents>();

            //Setup all necessary listeners
            cardManager.OnCardUsed += SpawnEntity;
        }
        public void SpawnEntity(EntityData entity, Vector2 position)
        {
            GameObject character = Instantiate<GameObject>(entity.playerPrefab, position, Quaternion.identity);
            SetupEntity(character, entity);
            mana.mana -= entity.cost;
        }
        private void SetupEntity(GameObject gameObject, EntityData entity)
        {
            switch (entity.entityType)
            {
                case EntityEnums.Type.Unit:
                    UnitData unitData = gameObject.GetComponent<UnitData>();
                    unitData.Activate(entity);
                    AddEntityToList(unitData);
                    HealthBar healthBar = gameObject.GetComponentInChildren<HealthBar>();
                    healthBar.StartHealthBar(entity.health);
                    break;
            }
        }
        private void AddEntityToList(EntityEvents entity)
        {
            allEntities.Add(entity);
            if (entity.type == EntityEnums.Type.Unit) playerUnits.Add(entity);
            else playerStructures.Add(entity);
        }
    }
}