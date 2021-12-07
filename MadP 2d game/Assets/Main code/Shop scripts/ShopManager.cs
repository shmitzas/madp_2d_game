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

        [Header("Game menu Buttons")]
        public Button gameMenuArenaButton;

        [Header("Upgrade and Buy system components")]
        public GameObject upgradeCard;
        public GameObject buyCard;
        public DeckData deckData;
        public int maxUpgradeLevel = 4;
        private RectTransform newCard;
        private List<Vector2> cardPositions;
        private List<RectTransform> upgradableCardsList;
        private List<RectTransform> buyableCardsList;

        private void Awake()
        {
            upgradableCardsList = new List<RectTransform>();
            buyableCardsList = new List<RectTransform>();
            cardPositions = new List<Vector2>();
            LoadCardPositions();
            OnShopLoad();
        }
        private void OnShopLoad()
        {
            buyMenu.gameObject.SetActive(false);
            upgradeButton.onClick.AddListener(EnableUpgradableCards);
            gameMenuArenaButton.onClick.AddListener(EnableUpgradableCards);
            buyButton.onClick.AddListener(EnableBuyableCards);
            LoadArenaDeck();
        }
        public void LoadArenaDeck()
        {
            upgradeMenu.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(true);
            LoadUpgradableCards();
            LoadBuyableCards();
        }
        private void LoadUpgradableCards()
        {
            int cardPosIndex = 0;
            for (int i = 0; i < deckData.cardData.Length; i++)
            {
                EntityData entity = deckData.cardData[i].entityData;
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
            for (int i = 0; i < deckData.cardData.Length; i++)
            {
                EntityData entity = deckData.cardData[i].entityData;
                if (entity.owned == false)
                {
                    newCard = Instantiate<GameObject>(buyCard, buyMenu).GetComponent<RectTransform>();
                    buyableCardsList.Add(newCard);
                    newCard.anchoredPosition3D = cardPositions[cardPosIndex];
                    BuyCard bCard = newCard.GetComponent<BuyCard>();
                    bCard.SetUpBuyableCard(entity, buyableCardsList[cardPosIndex]);
                    cardPosIndex++;
                    bCard.OnBuy += ReloadBuyMenu;
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
        private void ReloadBuyMenu(bool signal)
        {
            if (signal == true)
            {
                ClearShopMenu();
                LoadBuyableCards();
            }
        }
        private void ClearShopMenu()
        {
            for (int i = 0; i < upgradableCardsList.Count; i++)
                Destroy(upgradableCardsList[i].gameObject);
            for (int i = 0; i < buyableCardsList.Count; i++)
                Destroy(buyableCardsList[i].gameObject);
            upgradableCardsList.Clear();
            buyableCardsList.Clear();
        }
    }
}