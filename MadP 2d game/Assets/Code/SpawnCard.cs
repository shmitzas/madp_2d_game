using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MADP
{
    public class SpawnCard : MonoBehaviour
    {
        public bool enableSpawn { get; private set; }
        private Button selectCard;
        public PolygonCollider2D SpawnZone;
        public ManaRefil manaRefil;
        public PlacableEntities entity;
        private bool cardMoved;
        private void Start()
        {
            SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            CardSelection();
        }

        private void CardSelection()
        {

            selectCard = this.GetComponent<Button>();
            selectCard.onClick.AddListener(CardCheck);
        }
        private void CardCheck()
        {

            if (cardMoved == true)
            {
                this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 10);
                enableSpawn = false;
                cardMoved = false;
                manaRefil.mana += this.entity.cost;
                SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (manaRefil.setManaCounter >= this.entity.cost)
            {
                this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 10);
                cardMoved = true;
                enableSpawn = true;
                manaRefil.mana -= this.entity.cost;
                SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = true;

            }
            else
            {
                enableSpawn = false;
                Debug.Log("Not enough mana to spawn selected character");
            }
        }

        private void SpawnCharacter()
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
                if (hit.collider != null && hit.collider.gameObject.tag == "SpawnZone" && enableSpawn == true)
                {
                    //spawn entity
                    Debug.Log("Spawned " + entity.name);

                    //----- This creates a copy (not independent object) of entity prefab ----- NOT GOOD!
                    GameObject character = Instantiate<GameObject>(entity.playerPrefab, mousePos, Quaternion.identity);
                    //-----------------------------------------------------------------------------------

                    SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    enableSpawn = false;
                    cardMoved = false;
                    this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 10);
                }
            }
        }
        private void Update()
        {
            SpawnCharacter();
        }
    }
}