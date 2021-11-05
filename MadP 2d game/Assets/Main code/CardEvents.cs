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
        public GameObject managers;
        [HideInInspector] public Vector2 defaultCardPosition;
        [HideInInspector] public CardData cardData;
        [SerializeField] private Canvas canvas;
        private RectTransform rectTransform;
        public Text manaCost;
        public Image cardImage;

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
        }
        public void UpdateCardUI(float cost, Sprite newImage)
        {
            manaCost.text = cost.ToString();
            cardImage.sprite = newImage;
            this.gameObject.SetActive(true);
        }

        public void InitialiseWithData(CardData cData, int index)
        {
            cardData = cData;
            manaCost.text = cData.entityData[index].cost.ToString();
            cardImage.sprite = cData.cardImage;
            this.gameObject.SetActive(true);
        }
        
        private void Start() {
            defaultCardPosition = rectTransform.anchoredPosition;
            //Debug.Log("Card pos: " + defaultCardPosition);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            GameManager gameManager = managers.GetComponent<GameManager>();
            gameManager.SpawnEntity(cardData.entityData[0]);
            rectTransform.anchoredPosition = defaultCardPosition;
        }
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
  

        //------ UNITY ROYALE CODE ------

        // public UnityAction<int, Vector2> OnDragAction;
        // public UnityAction<int> OnTapDownAction, OnTapReleaseAction;

        // [HideInInspector] public int cardId;
        // [HideInInspector] public CardData cardData;
        // private CanvasGroup canvasGroup;

        //  private void Awake()
        // {
        //     canvasGroup = GetComponent<CanvasGroup>();
        // }

        //called by CardManager, it feeds CardData so this card can display the placeable's portrait
        // public void InitialiseWithData(CardData cData, int index)
        // {
        //     cardData = cData;
        //     manaCost.text = cData.entityData[index].cost.ToString();
        //     cardImage.sprite = cData.cardImage;
        //     this.gameObject.SetActive(true);
        // }

        // public void OnPointerDown(PointerEventData pointerEvent)
        // {
        //     if(OnTapDownAction != null)
        //         OnTapDownAction(cardId);
        // }

        // public void OnDrag(PointerEventData pointerEvent)
        // {
        //     if(OnDragAction != null)
        //         OnDragAction(cardId, pointerEvent.delta);
        // }

        // public void OnPointerUp(PointerEventData pointerEvent)
        // {
        //     if(OnTapReleaseAction != null)
        //         OnTapReleaseAction(cardId);
        // }

        // public void ChangeActiveState(bool isActive)
        // {
        //     canvasGroup.alpha = (isActive) ? .05f : 1f;
        // }
    }
}