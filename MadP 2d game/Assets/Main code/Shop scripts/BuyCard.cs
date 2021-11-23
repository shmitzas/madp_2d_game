using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class BuyCard : MonoBehaviour
    {
        public RewardsData rewards;
        public Image artowrk;
        public Text buyCost;
        public Button buyButton;
        public void SetUpBuyableCard(EntityData entity, RectTransform card)
        {
            buyButton.onClick.AddListener(delegate{Buy(entity, card);});
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
            }
        }
    }
}