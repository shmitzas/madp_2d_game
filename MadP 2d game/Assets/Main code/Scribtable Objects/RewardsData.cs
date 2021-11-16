using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    [CreateAssetMenu(fileName = "New Reward", menuName = "Rush N Destroy/Rewards")]
    public class RewardsData : ScriptableObject
    {
        [Header("Artwork")]
        public Sprite trophiesArtwork;
        public Sprite coinsArtwork;

        [Header("Data")]
        public int trophies = 0;
        public int coins = 0;
    }
}