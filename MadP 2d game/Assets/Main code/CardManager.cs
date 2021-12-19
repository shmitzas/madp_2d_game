using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

namespace RushNDestroy
{
    public class CardManager : MonoBehaviour
    {
        public Camera mainCamera; //public reference

        [Header("List of all card UI's in the scene")]
        public RectTransform[] defCardPositions; //default positions of all cards (used to create new cards or place unused card back to it's place)
        private RectTransform newCard;
        public RectTransform activeCards; //the UI panel that contains the actual playable cards
        public RectTransform cardsDeck; //the UI panel that contains all cards, the deck, and the dashboard (center aligned)
        private CardEvents[] cards;
        public DeckData playersDeck;
        public GameObject SpawnZone;
        public ManaRefill mana;
        private int cardsOnDeckCounter;
        private List<CardData> deckData = new List<CardData>();
        public UnityAction<EntityData, Vector2, EntityEnums.Faction> OnCardUsed;
        public GameObject cardPrefab;

        private void Awake()
        {
            cards = new CardEvents[4];
            LoadDeck();
        }
        private void LoadDeck()
        {
            for (int i = 0; i < playersDeck.cardData.Length; i++) //loads all cards from deck into a deckData List
            {
                if(playersDeck.cardData[i].entityData.owned)
                    deckData.Add(playersDeck.cardData[i]);
            }
            GenerateCardsOnDeck();
        }
        private void GenerateCardsOnDeck()
        {
            StartCoroutine(GenerateCards(0f));
            for (int i = 0; i < cards.Length; i++)
            {
                StartCoroutine(BringCardToDeck(i, 0f));
                StartCoroutine(GenerateCards(0f));
            }
        }
        private IEnumerator GenerateCards(float delay)
        {
            yield return new WaitForSeconds(delay);

            newCard = defCardPositions[0];
            newCard = Instantiate<GameObject>(cardPrefab, cardsDeck).GetComponent<RectTransform>();

            // newCard.SetParent(cardsDeck, true); //once card is created, it is set as child to CardDeck GameObject in Canvas
            newCard.pivot = defCardPositions[0].pivot;
            newCard.anchorMax = defCardPositions[0].anchorMax;
            newCard.anchorMin = defCardPositions[0].anchorMin;
            Vector2 newStartPos = new Vector2(defCardPositions[0].anchoredPosition.x, defCardPositions[0].anchoredPosition.y - 100f);
            Vector2 newDestinationPos = new Vector2(defCardPositions[0].anchoredPosition.x, defCardPositions[0].anchoredPosition.y);
            StartCoroutine(CardGenerationAnimation(newCard, newStartPos, newDestinationPos, 0.4f));
            newCard.localScale = defCardPositions[0].localScale;

            CardEvents cEvents = newCard.GetComponent<CardEvents>();
            int cardIndex = Random.Range(0, deckData.Count);
            cEvents.InitialiseWithData(deckData[cardIndex]);
        }
        private IEnumerator BringCardToDeck(int position, float delay)
        {
            yield return new WaitForSeconds(delay);
            newCard.SetParent(activeCards, true); //once card is brought to deck, it is set as child to ActiveCards GameObject in Canvas
            newCard.pivot = defCardPositions[position+1].pivot;
            newCard.anchorMax = defCardPositions[position+1].anchorMax;
            newCard.anchorMin = defCardPositions[position+1].anchorMin;
            Vector2 newStartPos = new Vector2(-260f, -10f);
            Vector2 newDestinationPos = new Vector2(defCardPositions[position + 1].anchoredPosition.x, defCardPositions[position + 1].anchoredPosition.y);
            StartCoroutine(CardGenerationAnimation(newCard, newStartPos, newDestinationPos, 0.4f));
            newCard.localScale = defCardPositions[position + 1].localScale;
            newCard.anchoredPosition = defCardPositions[position + 1].anchoredPosition;

            //store a reference to the CardEvents script in the array
            CardEvents cardEvents = newCard.GetComponent<CardEvents>();
            cardEvents.cardId = position;
            cards[position] = cardEvents;

            cardEvents.OnCardRelease += CardReleased;
            cardEvents.OnCardDrag += CardDragged;
        }
        private void CardDragged(int cardId, Vector2 dragAmount)
        {
            SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            cards[cardId].transform.Translate(dragAmount);
        }
        private void CardReleased(int cardId)
        {
            Vector2 mousePos;
            EntityData entity = cards[cardId].cardData.entityData;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            // Cast a ray through colliders
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            // If it hits a collider with a tag SpawnZone it will allow to spawn entities
            if (hit.collider != null && hit.collider.gameObject.tag == "SpawnZone" && mana.mana >= entity.cost)
            {
                OnCardUsed(cards[cardId].cardData.entityData, mousePos, EntityEnums.Faction.Player);
                Destroy(cards[cardId].gameObject);
                StartCoroutine(BringCardToDeck(cardId, 0f));
                StartCoroutine(GenerateCards(0f));
            }
            else
            {
                //Returns unused card back to it's place
                cards[cardId].GetComponent<RectTransform>().anchoredPosition = defCardPositions[cardId + 1].anchoredPosition3D;
            }
            SpawnZone.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        IEnumerator CardGenerationAnimation(RectTransform obj, Vector2 start, Vector2 destination, float duration)
        {
            float timer = 0;

            while (timer < duration)
            {
                obj.anchoredPosition = Vector2.Lerp(start, destination, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }
            transform.position = destination;
        }
    }
}
