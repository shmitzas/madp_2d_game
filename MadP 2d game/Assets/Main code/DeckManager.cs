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
        private List<Vector2> defaultCardPos = new List<Vector2>();
        // public CardEvents cardEvents;

        // private void Awake()
        // {
        //     cardManager = GetComponent<CardManager>();
        // }
        // private void Start()
        // {
        //     LoadDeck();
        //     Print();
        //     cardManager.cardEvents[0].InitialiseWithData(deckData[0], 0);
        // }
        // public void Print()
        // {

        // }
        // private void LoadDeck()
        // {
        //     for (int i = 0; i < targetDeck[activeDeckIndex].cardData.Length; i++)
        //     {
        //         deckData.Add(targetDeck[activeDeckIndex].cardData[i]);
        //         defaultCardPos.Add(cardManager.cardEvents[i].defaultCardPosition);
        //         Debug.Log("Pos from CardEvents: " + cardManager.cardEvents[0].defaultCardPosition);
        //         Debug.Log("Pos from PosList: " + defaultCardPos[i]);
        //     }
        // }

        // private void UpdateCard(int usedCardIndex)
        // {
        //     cardManager.cardEvents[0].InitialiseWithData(deckData[0], 0);
        // }
    }
}
