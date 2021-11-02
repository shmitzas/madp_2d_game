using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class CardChanger : MonoBehaviour
    {
        public Text manaCost;
        public Image cardImage;
        public void UpdateCardUI(float cost, Sprite newImage)
        {
            manaCost.text = cost.ToString();
            cardImage.sprite = newImage;
            this.gameObject.SetActive(true);
        }
    }
}