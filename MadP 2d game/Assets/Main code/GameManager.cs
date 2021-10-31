using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RushNDestroy
{
    public class GameManager : MonoBehaviour
    {
        public GameObject entityPrefab;
        private List<EntityEvents> playerUnits, aiUnits;
        private List<EntityEvents> playerStructures, aiStructures;
        private List<EntityEvents> allEntities;
        public EntityData entityData;

        private void Awake()
        {
            playerUnits = new List<EntityEvents>();
            playerStructures = new List<EntityEvents>();
            allEntities = new List<EntityEvents>();
        }

        private void Update()
        {
            SpawnEntity(entityData);
        }

        private void SpawnEntity(EntityData entity)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                // Cast a ray through colliders
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                // If it hits a collider with a tag SpawnZone it will allow to spawn entities
                if (hit.collider != null && hit.collider.gameObject.tag == "SpawnZone"/* && enableSpawn == true*/)
                {
                    //spawn entity


                    //Debug.Log("Spawned " + entity.name);
                    //----- This creates a linked copy (not independent object) of entity prefab ----- NOT GOOD!
                    GameObject character = Instantiate<GameObject>(entityPrefab, mousePos, Quaternion.identity);
                    SetupEntity(character, entity);
                    //-----------------------------------------------------------------------------------

                    //SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    //enableSpawn = false;
                    //cardMoved = false;
                    //this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 10);
                }
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