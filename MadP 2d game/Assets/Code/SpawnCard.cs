using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCard : MonoBehaviour
{
    public GameObject entityPrefab;
    public bool enableSpawn { get; private set; }
    private Button selectCard;
    public PolygonCollider2D SpawnZone;
    public ManaRefil manaRefil;
    public entities_data entity;
    public float spawnCost { get; private set; }
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
        if (manaRefil.setManaCounter >= this.entity.cost)
        {
            SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            enableSpawn = true;
        }
        else
        {
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
                GameObject a = Instantiate(entityPrefab) as GameObject;
                a.transform.position = new Vector2(mousePos.x, mousePos.y);
                manaRefil.mana -= this.entity.cost;
                SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                enableSpawn = false;
            }
        }
    }
    private void Update()
    {
        SpawnCharacter();
    }
}
