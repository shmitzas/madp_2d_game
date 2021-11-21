using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class UpgradeManager : MonoBehaviour
    {
        public UpdateRewards updateRewards;
        public RewardsData rewards;
        public EntityData entity;
        public Image artowrk;
        public Text upgradeCounter;
        public Text upgradeCost;
        public Button upgradeButton;

        private void Start()
        {
            upgradeButton.onClick.AddListener(Upgrade);
            artowrk.sprite = entity.artwork;
            upgradeCounter.text = entity.upgradeLevel.ToString();
            upgradeCost.text = "Cost: " + entity.upgradeCost.ToString();
        }
        private void Upgrade()
        {
            if (entity.upgradeLevel < 5 && rewards.coins >= entity.upgradeCost)
            {
                entity.attackDamage += 1;
                entity.health += 2.5f;
                entity.speed += 0.2f;
                entity.upgradeLevel += 1;
                entity.upgradeCost += entity.upgradeCost * entity.upgradeLevel;
                rewards.coins -= entity.upgradeCost;
                upgradeCounter.text = entity.upgradeLevel.ToString();
                upgradeCost.text = "Cost: " + entity.upgradeCost.ToString();
                updateRewards.UpdateRewardsText();
            }
        }
        private void Update() {
            if(entity.upgradeLevel == 4)
            {
                upgradeButton.gameObject.SetActive(false);
                upgradeCost.gameObject.SetActive(false);
            }
        }
    }
}