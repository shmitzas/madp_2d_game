using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    public class DeckManager : MonoBehaviour
    {
        public DeckData[] targetDeck;
        private List<CardData> deckData = new List<CardData>();
        public int activeDeckIndex { get; set; }
        private CardManager cardManager;
        private GameObject findCard;
        // public CardEvents cardEvents;

        private void Awake()
        {
            cardManager = GetComponent<CardManager>();
        }
        private void Start()
        {
            LoadDeck();
            Print();
            cardManager.cardEvents[0].InitialiseWithData(deckData[0], 0);
            
            // cardChanger.UpdateCardUI(deckData[0].entityData[0].cost, deckData[0].cardImage);
        }
        public void Print()
        {
            // Debug.Log("Cards in deck: " + deckData.Count);
        }
        private void LoadDeck()
        {
            for (int i = 0; i < targetDeck[activeDeckIndex].cardData.Length; i++)
                deckData.Add(targetDeck[activeDeckIndex].cardData[i]);
        }

        private void UpdateCard(List<CardData> deckData)
        {

        }
    }
}
