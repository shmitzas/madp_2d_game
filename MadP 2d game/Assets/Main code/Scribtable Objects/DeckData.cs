using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    [CreateAssetMenu(fileName = "New Deck", menuName = "Rush N Destroy/Deck Data")]
    public class DeckData : ScriptableObject
    {
        [Header("List of all cards in deck")]
        public CardData[] cardData;
    }
}