using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class CardEvents : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [HideInInspector] public int cardId;
        public UnityAction<int> OnCardRelease;
        public UnityAction<int, Vector2> OnCardDrag;
        //public UnityAction OnCardSelect;
        [HideInInspector] public CardData cardData;
        public Text manaCost;
        public Image cardImage;

        public void UpdateCardUI(float cost, Sprite newImage)
        {
            manaCost.text = cost.ToString();
            cardImage.sprite = newImage;
            this.gameObject.SetActive(true);
        }
        public void InitialiseWithData(CardData cData)
        {
            cardData = cData;
            manaCost.text = cData.entityData.cost.ToString();
            cardImage.sprite = cData.cardImage;
            this.gameObject.SetActive(true);
        }
        public void OnPointerDown(PointerEventData eventData){}
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if(OnCardRelease != null)
                OnCardRelease(cardId);
            //rectTransform.anchoredPosition = defaultCardPosition;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if(OnCardDrag != null)
                OnCardDrag(cardId, eventData.delta);
        }
    }
}