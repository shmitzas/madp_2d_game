using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class SaveManager : MonoBehaviour
    {
        public RewardsData rewardsData;
        public DeckData deckData;
        public UpdateRewards updateRewards;
        [SerializeField] private EntityDataToClassList entities;
        public void Save()
        {
            // saves data of rewards
            RewardsDataToClass rewards = new RewardsDataToClass(rewardsData);
            SaveSystem.SaveData("rewards", rewards);

            // saves data of all characters in the deck
            for (int i = 0; i < deckData.cardData.Length; i++)
            {
                EntityDataToClass tempEntity = new EntityDataToClass(deckData.cardData[i].entityData);
                entities.list.Add(tempEntity);
            }
            if(entities.list.Count == deckData.cardData.Length)
                SaveSystem.SaveData("entities", entities);
        }
        public void LoadSave()
        {
            EntityDataToClassList entitiesSave = SaveSystem.LoadEntities("entities");
            for (int i = 0; i < deckData.cardData.Length; i++)
            {
                deckData.cardData[i].entityData.health = entitiesSave.list[i].health;
                deckData.cardData[i].entityData.attackDamage = entitiesSave.list[i].attackDamage;
                deckData.cardData[i].entityData.attackRatio = entitiesSave.list[i].attackRatio;
                deckData.cardData[i].entityData.speed = entitiesSave.list[i].speed;
                deckData.cardData[i].entityData.upgradeCost = entitiesSave.list[i].upgradeCost;
                deckData.cardData[i].entityData.upgradeLevel = entitiesSave.list[i].upgradeLevel;
                deckData.cardData[i].entityData.buyCost = entitiesSave.list[i].buyCost;
                deckData.cardData[i].entityData.owned = entitiesSave.list[i].owned;
            }
            RewardsDataToClass rewardsSave = SaveSystem.LoadRewards("rewards");
            rewardsData.coins = rewardsSave.coins;
            rewardsData.trophies = rewardsSave.trophies;
            updateRewards.coinsAmount.text = rewardsData.coins.ToString();
            updateRewards.trophiesAmount.text = rewardsData.trophies.ToString();
        }
    }
}
