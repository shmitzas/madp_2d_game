using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class UpdateRewards : MonoBehaviour
    {
        public RewardsData rewards;
        public Image trophiesImage;
        public Text trophiesAmount;
        public Image coinsImage;
        public Text coinsAmount;

        private void Awake()
        {
            trophiesImage.sprite = rewards.trophiesArtwork;
            coinsImage.sprite = rewards.coinsArtwork;
        }
        public void UpdateRewardsAmount(int trophies, int coins)
        {
            rewards.trophies += trophies;
            rewards.coins += coins;
        }

        private void Update()
        {
            trophiesAmount.text = rewards.trophies.ToString();
            coinsAmount.text = rewards.coins.ToString();
        }
    }
}