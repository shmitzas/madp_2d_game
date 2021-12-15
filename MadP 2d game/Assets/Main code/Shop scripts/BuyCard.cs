using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace RushNDestroy
{
    public class BuyCard : MonoBehaviour
    {
        public UnityAction<EntityData> OnShowInfo;
        public UnityAction<bool> OnBuy;
        public RewardsData rewards;
        public Image artowrk;
        public Text buyCost;
        public Button buyButton;

        [Header("Info components")]
        public Button infoButton;
        public void SetUpBuyableCard(EntityData entity, RectTransform card)
        {
            buyButton.onClick.AddListener(delegate{Buy(entity, card);});
            infoButton.onClick.AddListener(delegate { ShowInfo(entity); });
            artowrk.sprite = entity.artwork;
            buyCost.text = "Cost: " + entity.buyCost.ToString();
        }
        private void Buy(EntityData entity, RectTransform card)
        {
            if (rewards.coins >= entity.buyCost)
            {
                rewards.coins -= entity.buyCost;
                entity.owned = true;
                card.gameObject.SetActive(false);
                if(OnBuy != null)
                    OnBuy(true);
            }
        }
        private void ShowInfo(EntityData entity)
        {
            OnShowInfo(entity);
        }
    }
}