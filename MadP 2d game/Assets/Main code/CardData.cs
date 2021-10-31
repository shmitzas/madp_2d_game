using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card data")]
    public class CardData : ScriptableObject
    {
        [Header("Card image")]
        public Sprite cardImage;

        [Header("List of Placeables")]
        public EntityData[] entityData;
    }
}
