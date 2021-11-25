using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class UpgradeCard : MonoBehaviour
    {
        public RewardsData rewards;
        public Image artowrk;
        public Text upgradeCounter;
        public Text upgradeCost;
        public Button upgradeButton;
        public void SetUpCard(EntityData entity, int maxUpgradeLevel)
        {
            if (entity.upgradeLevel == maxUpgradeLevel)
            {
                upgradeButton.gameObject.SetActive(false);
                upgradeCost.gameObject.SetActive(false);
            }
            upgradeButton.onClick.AddListener(delegate { Upgrade(entity, maxUpgradeLevel); });
            artowrk.sprite = entity.artwork;
            upgradeCounter.text = entity.upgradeLevel.ToString();
            upgradeCost.text = "Cost: " + entity.upgradeCost.ToString();
        }
        private void Upgrade(EntityData entity, int maxUpgradeLevel)
        {
            if (entity.upgradeLevel < maxUpgradeLevel && rewards.coins >= entity.upgradeCost)
            {
                entity.attackDamage += 1;
                entity.health += 2.5f;
                entity.speed += 0.2f;
                if (entity.upgradeLevel == 0)
                    entity.upgradeCost += entity.upgradeCost * 2;
                else
                    entity.upgradeCost += entity.upgradeCost * entity.upgradeLevel;
                rewards.coins -= entity.upgradeCost;
                entity.upgradeLevel++;
                upgradeCounter.text = entity.upgradeLevel.ToString();
                upgradeCost.text = "Cost: " + entity.upgradeCost.ToString();

            }
            if (entity.upgradeLevel == maxUpgradeLevel)
            {
                upgradeButton.gameObject.SetActive(false);
                upgradeCost.gameObject.SetActive(false);
            }
        }
    }
}