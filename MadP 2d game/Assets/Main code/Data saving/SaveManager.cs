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
        //public UpdateRewards updateRewards;
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
            SaveSystem.SaveData("entities", entities);
            entities.list.Clear();
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
        }
        public void NewGame()
        {
            rewardsData.coins = 1000;
            rewardsData.trophies = 100;
            for (int i = 0; i < deckData.cardData.Length; i++)
            {
                switch (i)
                {
                    case 0: //Default Warrior data
                        deckData.cardData[i].entityData.health = 30f;
                        deckData.cardData[i].entityData.attackDamage = 2f;
                        deckData.cardData[i].entityData.attackRatio = 2f;
                        deckData.cardData[i].entityData.speed = 2f;
                        deckData.cardData[i].entityData.upgradeCost = 30;
                        deckData.cardData[i].entityData.upgradeLevel = 0;
                        deckData.cardData[i].entityData.buyCost = 30;
                        deckData.cardData[i].entityData.owned = true;
                        break;
                    case 1: //Default Archer data
                        deckData.cardData[i].entityData.health = 20f;
                        deckData.cardData[i].entityData.attackDamage = 5f;
                        deckData.cardData[i].entityData.attackRatio = 2f;
                        deckData.cardData[i].entityData.speed = 1.6f;
                        deckData.cardData[i].entityData.upgradeCost = 30;
                        deckData.cardData[i].entityData.upgradeLevel = 0;
                        deckData.cardData[i].entityData.buyCost = 50;
                        deckData.cardData[i].entityData.owned = true;
                        break;
                    case 2: //Default Zeppelin data
                        deckData.cardData[i].entityData.health = 80f;
                        deckData.cardData[i].entityData.attackDamage = 60f;
                        deckData.cardData[i].entityData.attackRatio = 4f;
                        deckData.cardData[i].entityData.speed = 1.2f;
                        deckData.cardData[i].entityData.upgradeCost = 150;
                        deckData.cardData[i].entityData.upgradeLevel = 0;
                        deckData.cardData[i].entityData.buyCost = 200;
                        deckData.cardData[i].entityData.owned = false;
                        break;
                    case 3: //Default Dragon data
                        deckData.cardData[i].entityData.health = 150f;
                        deckData.cardData[i].entityData.attackDamage = 20f;
                        deckData.cardData[i].entityData.attackRatio = 3f;
                        deckData.cardData[i].entityData.speed = 1.2f;
                        deckData.cardData[i].entityData.upgradeCost = 250;
                        deckData.cardData[i].entityData.upgradeLevel = 0;
                        deckData.cardData[i].entityData.buyCost = 300;
                        deckData.cardData[i].entityData.owned = false;
                        break;
                }
            }
            Save();
        }
    }
}
