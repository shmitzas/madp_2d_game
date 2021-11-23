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

        [Header("Upgrade and Buy system components")]
        public GameObject upgradeCard;
        public GameObject buyCard;
        public DecksList decksList;
        public int maxUpgradeLevel = 4;
        private RectTransform newCard;
        private List<Vector2> cardPositions;
        private List<RectTransform> upgradableCardsList;
        private List<RectTransform> buyableCardsList;

        private void Start()
        {
            upgradableCardsList = new List<RectTransform>();
            buyableCardsList = new List<RectTransform>();
            cardPositions = new List<Vector2>();
            LoadCardPositions();
            LoadUpgradableCards();
            LoadBuyableCards();
            upgradeMenu.gameObject.SetActive(false);
            buyMenu.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            backToArenaDeckMenu.gameObject.SetActive(false);
            arenaSelectionContainer.gameObject.SetActive(true);
            upgradeButton.onClick.AddListener(EnableUpgradableCards);
            buyButton.onClick.AddListener(EnableBuyableCards);
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

        private void LoadUpgradableCards()
        {
            int cardPosIndex = 0;
            for (int i = 0; i < decksList.deckData[arenaIndex].cardData.Length; i++)
            {
                EntityData entity = decksList.deckData[arenaIndex].cardData[i].entityData;
                if (entity.owned == true)
                {
                    newCard = Instantiate<GameObject>(upgradeCard, upgradeMenu).GetComponent<RectTransform>();
                    upgradableCardsList.Add(newCard);
                    newCard.anchoredPosition3D = cardPositions[cardPosIndex];
                    UpgradeCard uCard = newCard.GetComponent<UpgradeCard>();
                    uCard.SetUpCard(entity, maxUpgradeLevel);
                    cardPosIndex++;
                }
            }
        }
        private void EnableUpgradableCards()
        {
            ClearShopMenu();
            LoadUpgradableCards();
            upgradeMenu.gameObject.SetActive(true);
            buyMenu.gameObject.SetActive(false);
        }
        private void LoadBuyableCards()
        {
            int cardPosIndex = 0;
            for (int i = 0; i < decksList.deckData[arenaIndex].cardData.Length; i++)
            {
                EntityData entity = decksList.deckData[arenaIndex].cardData[i].entityData;
                if (entity.owned == false)
                {
                    newCard = Instantiate<GameObject>(buyCard, buyMenu).GetComponent<RectTransform>();
                    buyableCardsList.Add(newCard);
                    newCard.anchoredPosition3D = cardPositions[cardPosIndex];
                    BuyCard bCard = newCard.GetComponent<BuyCard>();
                    bCard.SetUpBuyableCard(entity, buyableCardsList[cardPosIndex]);
                    cardPosIndex++;
                }
            }
        }

        private void EnableBuyableCards()
        {
            ClearShopMenu();
            LoadBuyableCards();
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
                switch (posIndex)
                {
                    case 0:
                        cardPositions.Add(new Vector2(-280, 180));
                        break;
                    case 1:
                        cardPositions.Add(new Vector2(-140, 180));
                        break;
                    case 2:
                        cardPositions.Add(new Vector2(0, 180));
                        break;
                    case 3:
                        cardPositions.Add(new Vector2(140, 180));
                        break;
                    case 4:
                        cardPositions.Add(new Vector2(280, 180));
                        break;
                    case 5:
                        cardPositions.Add(new Vector2(-280, -20));
                        break;
                    case 6:
                        cardPositions.Add(new Vector2(-140, -20));
                        break;
                    case 7:
                        cardPositions.Add(new Vector2(0, -20));
                        break;
                    case 8:
                        cardPositions.Add(new Vector2(140, -20));
                        break;
                    case 9:
                        cardPositions.Add(new Vector2(280, -20));
                        break;
                }
                posIndex++;
            }
        }
        private void ClearShopMenu(){
            for(int i=0; i<upgradableCardsList.Count; i++)
            Destroy(upgradableCardsList[i].gameObject);
            for(int i=0; i<buyableCardsList.Count; i++)
            Destroy(buyableCardsList[i].gameObject);
            upgradableCardsList.Clear();
            buyableCardsList.Clear();
        }
    }
}