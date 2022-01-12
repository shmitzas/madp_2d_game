using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RushNDestroy
{
    public class AISpawning : MonoBehaviour
    {
        public DeckData aiDeck;
        public ManaRefill manaRefill;
        public UnityAction<EntityData, Vector2, EntityEnums.Faction> SpawnAIUnit;
        private float aiMana = 0;
        private EntityData card;
        private GameManager gameManager;
        private List<CardData> deckData = new List<CardData>();
        public int levelID;
        private Vector2 spawnLocation;
        private List<Vector2> locations;

        private void Awake()
        {
            gameManager = GetComponent<GameManager>();
            for (int i = 0; i < aiDeck.cardData.Length; i++) //loads all cards from deck into a deckData List
            {
                if (aiDeck.cardData[i].entityData.owned)
                    deckData.Add(aiDeck.cardData[i]);
            }
            LoadAICard();
        }
        private void FixedUpdate()
        {


            if (aiMana < 10)
                aiMana += manaRefill.addMana / 2;

            if (aiMana >= card.cost)
            {   int locationID;
                switch (levelID)
                {
                    
                    case 1:
                        //spawnzone locations coordinates order (minX, maxX), (minY, maxY)
                        spawnLocation = new Vector2(Random.Range(5.7f, 10.4f), Random.Range(3.4f, -4.3f));
                        break;
                    case 2:
                        //spawnzone locations coordinates order (minX, maxX), (minY, maxY)
                        spawnLocation = new Vector2(Random.Range(5.7f, 10.4f), Random.Range(3.4f, -4.3f));
                        break;
                    case 3:
                        locations = new List<Vector2>();
                        //spawnzone locations coordinates order (minX, maxX), (minY, maxY)
                        locations.Add(new Vector2(Random.Range(2.44f, 12.6f), Random.Range(6.6f, -2.3f)));
                        locations.Add(new Vector2(Random.Range(0f, 12.4f), Random.Range(-2.4f, -7.3f)));
                        locationID = Random.Range(0, locations.Count);
                        spawnLocation = locations[locationID];
                        break;
                    case 4:
                        locations = new List<Vector2>();
                        //spawnzone locations coordinates order (minX, maxX), (minY, maxY)
                        locations.Add(new Vector2(Random.Range(-8.38f, 5f), Random.Range(-5.24f, -9.71f)));
                        locations.Add(new Vector2(Random.Range(5.4f, 7f), Random.Range(-2.35f, -10.72f)));
                        locations.Add(new Vector2(Random.Range(7.4f, 14.7f), Random.Range(4.72f, 0.6f)));
                        locations.Add(new Vector2(Random.Range(7.57f, 23.6f), Random.Range(-1.44f, -11.73f)));
                        locations.Add(new Vector2(Random.Range(7.4f, 21.6f), Random.Range(0.4f, -13.5f)));
                        locations.Add(new Vector2(Random.Range(7.4f, 18.48f), Random.Range(0.4f, -15.62f)));
                        locationID = Random.Range(0, locations.Count);
                        spawnLocation = locations[locationID];
                        break;
                    case 5:
                        locations = new List<Vector2>();
                        //spawnzone locations coordinates order (minX, maxX), (minY, maxY)
                        locations.Add(new Vector2(Random.Range(2.44f, 12.6f), Random.Range(6.6f, -2.3f)));
                        locations.Add(new Vector2(Random.Range(0f, 12.4f), Random.Range(-2.4f, -7.3f)));
                        locationID = Random.Range(0, locations.Count);
                        spawnLocation = locations[locationID];
                        break;
                }
            SpawnAIUnit(card, spawnLocation, EntityEnums.Faction.Enemy);
            aiMana -= card.cost;
            LoadAICard();
        }
    }
    private void LoadAICard()
    {
        int cardIndex = Random.Range(0, deckData.Count);
        //Debug.Log(cardIndex);
        card = deckData[cardIndex].entityData;
    }
}
}
