using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    [CreateAssetMenu(fileName = "New Decks List", menuName = "Rush N Destroy/Decks List")]
    public class DecksList : ScriptableObject
    {
        [Header("List of all decks in game")]
        public DeckData[] deckData;
    }
}