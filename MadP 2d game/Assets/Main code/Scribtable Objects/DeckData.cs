using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    [CreateAssetMenu(fileName = "New Deck", menuName = "Rush N Destroy/Deck Data")]
    public class DeckData : ScriptableObject
    {
        private CardData[] cardData;
        private int currentCard = 0;

        public void LoadCardDeck(List<CardData> cardDataList)
        {
            int totalCards = cardDataList.Count;
            cardData = new CardData[totalCards];
            for (int i = 0; i < totalCards; i++)
                cardData[i] = cardDataList[i];
        }

        public void ShuffleCards()
        {
            //TODO: shuffle cards
        }

        public CardData GetNextCardFromDeck()
        {
            currentCard++;
            if (currentCard >= cardData.Length)
                currentCard = 0;
            return cardData[currentCard];
        }
    }
}