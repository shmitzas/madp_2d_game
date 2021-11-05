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
        //public RectTransform newCardPosRefference; //Position refference for new cards that have to end up on the deck
        public RectTransform activeCards; //the UI panel that contains the actual playable cards
        public RectTransform cardsDeck; //the UI panel that contains all cards, the deck, and the dashboard (center aligned)
        private CardEvents cardEvents;
        public DeckData playersDeck;
        private int cardsOnDeckCounter;
        //public UnityAction<EntityData, Vector2> OnCardUsed;
        //private List<Vector2> defaultCardPos = new List<Vector2>();
        private List<CardData> deckData = new List<CardData>();
        public GameObject cardPrefab;

        private void Awake()
        {
            cardEvents = GetComponentInChildren<CardEvents>();
        }
        private void Start()
        {
            LoadDeck();
            //cardEvents = new CardEvents[4]; //this is the amount of cards in active deck
        }
        private void LoadDeck()
        {
            for (int i = 0; i < playersDeck.cardData.Length; i++) //loads all cards from deck into a deckData List
            {
                deckData.Add(playersDeck.cardData[i]);
            }
            GenerateCardsOnDeck();
        }
        private void GenerateCardsOnDeck()
        {
            StartCoroutine(GenerateCards(0.4f));
            for (int i = 0; i < 5; i++)
            {
                StartCoroutine(GenerateCards(0.4f+i));
            }
        }
        private IEnumerator GenerateCards(float delay)
        {
            yield return new WaitForSeconds(delay);

            newCard = defCardPositions[0];
            newCard = Instantiate<GameObject>(cardPrefab, activeCards).GetComponent<RectTransform>();

            newCard.SetParent(cardsDeck, true); //once card is created, it is set as child to CardDeck GameObject in Canvas

            Vector2 newStartPos = new Vector2(defCardPositions[0].anchoredPosition.x, defCardPositions[0].anchoredPosition.y-300f);
            Vector2 newDestinationPos = new Vector2(defCardPositions[0].anchoredPosition.x, defCardPositions[0].anchoredPosition.y);
            StartCoroutine(CardGenerationAnimation(newCard, newStartPos, newDestinationPos, 0.2f));
            newCard.localScale = defCardPositions[0].localScale;

            Random.Range(0, deckData.Count);
            CardEvents cEvents = newCard.GetComponent<CardEvents>();
            int cardIndex = Random.Range(0, deckData.Count);
            cEvents.InitialiseWithData(deckData[cardIndex]);
        }
        private IEnumerator BringCardToDeck(float delay)
        {
            yield return new WaitForSeconds(delay);
            newCard.SetParent(activeCards, true); //once card is brought to deck, it is set as child to ActiveCards GameObject in Canvas
            Vector2 newStartPos = new Vector2(-257.3f, -15.3f);
            Vector2 newDestinationPos = new Vector2(defCardPositions[0 + 1].anchoredPosition.x, defCardPositions[0 + 1].anchoredPosition.y);
            StartCoroutine(CardGenerationAnimation(newCard, newStartPos, newDestinationPos, 0.5f));
            newCard.localScale = defCardPositions[0 + 1].localScale;

            cardEvents.OnCardRelease += CardReleased;
        }
        private void CardReleased(int cardId)
        {

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
