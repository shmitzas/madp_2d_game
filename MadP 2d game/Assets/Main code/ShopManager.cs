using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Upgrades and Buy Buttons")]
        public Button upgradeButton;
        public RectTransform upgradeMenu;
        public Button buyButton;
        public RectTransform buyMenu;

        [Header("Arena Buttons")]
        public GameObject arenaSelectionContainer;
        public Button backToArenaDeckMenu;
        private int arenaIndex;

        [Header("Rewards and Card system components")]
        public UpdateRewards updateRewards;
        public RewardsData rewards;
        public DecksList decksList;

        [Header("Rewards and Card system components")]
        public GameObject upgradeCard;
        public GameObject buyCard;
        public int maxUpgradeLevel = 4;
        private RectTransform newCard;
        private Vector2[] cardPositions;

        private void Start()
        {
            LoadCardPositions();
            upgradeButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            backToArenaDeckMenu.gameObject.SetActive(false);
            upgradeButton.onClick.AddListener(LoadDeckCards);
            buyButton.onClick.AddListener(LoadNotOwnedDeckCards);
            backToArenaDeckMenu.onClick.AddListener(ShowOnlyArenaDeckSelection);
        }
        public void LoadArenaDeck(int arenaNum)
        {
            arenaIndex = arenaNum - 1;
            arenaSelectionContainer.gameObject.SetActive(false);

            backToArenaDeckMenu.gameObject.SetActive(true);
            upgradeMenu.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(true);
        }

        private void LoadDeckCards()
        {
            upgradeMenu.gameObject.SetActive(true);
            buyMenu.gameObject.SetActive(false);
            for (int i = 0; i < decksList.deckData[arenaIndex].cardData.Length; i++)
            {
                newCard = Instantiate<GameObject>(upgradeCard, upgradeMenu).GetComponent<RectTransform>();
                if (i <= 5)
                {
                    newCard.anchoredPosition3D = new Vector2(-240, 0);
                }
            }
        }
        private void LoadNotOwnedDeckCards()
        {
            upgradeMenu.gameObject.SetActive(false);
            buyMenu.gameObject.SetActive(true);
        }
        private void ShowOnlyArenaDeckSelection()
        {
            upgradeButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            backToArenaDeckMenu.gameObject.SetActive(false);
            upgradeMenu.gameObject.SetActive(false);
            buyMenu.gameObject.SetActive(false);
            arenaSelectionContainer.gameObject.SetActive(true);
        }
        private void LoadCardPositions()
        {
            int posIndex = 0;
            Vector2 firstRowPos = new Vector2(-280, 180);
            Vector2 secondRowPos = new Vector2(-280, -20);
            while (posIndex < 10)
            {
                if (posIndex == 0)
                    cardPositions[posIndex] = firstRowPos;
                else if((posIndex > 0 && posIndex < 6))
                {
                    cardPositions[posIndex] = cardPositions[posIndex - 1];
                    cardPositions[posIndex].x += 140;
                }
                else if (posIndex == 6)
                    cardPositions[posIndex] = secondRowPos;
                else if((posIndex > 6))
                {
                    cardPositions[posIndex] = cardPositions[posIndex - 1];
                    cardPositions[posIndex].x += 140;
                }
            }
            for(int i = 0; i<cardPositions.Length; i++)
                Debug.Log(cardPositions[i]);
        }

        // private void Start()
        // {
        //     upgradeButton.onClick.AddListener(Upgrade);
        //     artowrk.sprite = entity.artwork;
        //     upgradeCounter.text = entity.upgradeLevel.ToString();
        //     upgradeCost.text = "Cost: " + entity.upgradeCost.ToString();
        // }
        private void Upgrade(EntityData entity)
        {
            if (entity.upgradeLevel < maxUpgradeLevel && rewards.coins >= entity.upgradeCost)
            {
                entity.attackDamage += 1;
                entity.health += 2.5f;
                entity.speed += 0.2f;
                entity.upgradeLevel += 1;
                entity.upgradeCost += entity.upgradeCost * entity.upgradeLevel;
                rewards.coins -= entity.upgradeCost;
                // upgradeCounter.text = entity.upgradeLevel.ToString();
                // upgradeCost.text = "Cost: " + entity.upgradeCost.ToString();
                updateRewards.UpdateRewardsText();
            }
        }
        // private void Update()
        // {
        //     if (entity.upgradeLevel == maxUpgradeLevel)
        //     {
        //         upgradeButton.gameObject.SetActive(false);
        //         upgradeCost.gameObject.SetActive(false);
        //     }
        // }
    }
}