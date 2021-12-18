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
        public int trophies;
        public int coins;
    }
    // [System.Serializable]
    // public class RewardsDataToClassList
    // {
    //     public List<RewardsDataToClass> list;
    // }
    [System.Serializable]
    public class RewardsDataToClass
    {
        public int coins;
        public int trophies;
        public RewardsDataToClass(RewardsData rewardsData)
        {
            coins = rewardsData.coins;
            trophies = rewardsData.trophies;
        }
    }
}