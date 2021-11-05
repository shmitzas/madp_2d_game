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
        public ManaRefil mana;

        private void Awake()
        {
            cardManager = GetComponent<CardManager>();
            //cardManager.activeDeckIndex = 0;
            playerUnits = new List<EntityEvents>();
            playerStructures = new List<EntityEvents>();
            allEntities = new List<EntityEvents>();

            //cardManager.OnCardUsed += SpawnEntity;
        }

        private void Update()
        {
            //SpawnEntity(entityData);
        }

        public void SpawnEntity(EntityData entity, Vector2 position)
        {
                Vector2 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                // Cast a ray through colliders
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                // If it hits a collider with a tag SpawnZone it will allow to spawn entities
                if (hit.collider != null && hit.collider.gameObject.tag == "SpawnZone" && mana.mana >= entity.cost)
                {
                    //mousePos.y += 1;
                    GameObject character = Instantiate<GameObject>(entity.playerPrefab, mousePos, Quaternion.identity);
                    SetupEntity(character, entity);
                    mana.mana -= entity.cost;

                    //SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    //enableSpawn = false;
                    //cardMoved = false;
                    //this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 10);
                }
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