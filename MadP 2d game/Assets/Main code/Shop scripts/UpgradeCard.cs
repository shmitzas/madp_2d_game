using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace RushNDestroy
{
    public class UpgradeCard : MonoBehaviour
    {
        public UnityAction<EntityData> OnShowInfo;
        [Header("Upgrade components")]
        public RewardsData rewards;
        public Image artowrk;
        public Text upgradeCounter;
        public Text upgradeCost;
        public Button upgradeButton;
        public SaveManager saveManager;
        [Header("Info components")]
        public Button infoButton;

        public void SetUpCard(EntityData entity, int maxUpgradeLevel)
        {
            if (entity.upgradeLevel == maxUpgradeLevel)
            {
                upgradeButton.gameObject.SetActive(false);
                upgradeCost.gameObject.SetActive(false);
            }
            upgradeButton.onClick.AddListener(delegate { Upgrade(entity, maxUpgradeLevel); });
            infoButton.onClick.AddListener(delegate { ShowInfo(entity); });
            artowrk.sprite = entity.artwork;
            upgradeCounter.text = entity.upgradeLevel.ToString();
            upgradeCost.text = "Cost: " + entity.upgradeCost.ToString();
        }
        private void Upgrade(EntityData entity, int maxUpgradeLevel)
        {
            if (entity.upgradeLevel < maxUpgradeLevel && rewards.coins >= entity.upgradeCost)
            {
                entity.attackDamage = entity.attackDamage*1.05f;
                entity.attackRatio -= entity.attackRatio * 0.1f;
                entity.health = entity.health *1.05f;
                entity.speed = entity.speed *1.1f;
                rewards.coins -= entity.upgradeCost;
                entity.upgradeCost = entity.upgradeCost * 2;
                entity.upgradeLevel++;
                upgradeCounter.text = entity.upgradeLevel.ToString();
                upgradeCost.text = "Cost: " + entity.upgradeCost.ToString();

            }
            if (entity.upgradeLevel == maxUpgradeLevel)
            {
                upgradeButton.gameObject.SetActive(false);
                upgradeCost.gameObject.SetActive(false);
            }
            saveManager.Save();
        }
        private void ShowInfo(EntityData entity)
        {
            OnShowInfo(entity);
        }
    }
}