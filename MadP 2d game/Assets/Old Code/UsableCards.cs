using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MADP
{
    public class UsableCards : MonoBehaviour
    {
        private bool enableGrayscale = true;
        public Material deafultMaterial;
        public Material grayscaleMaterial;
        public Button selectCard;
        public Image cardImage;
        public ManaRefil manaRefil;
        public PlacableEntities entity;

        private void Start()
        {
            selectCard.GetComponent<Button>();
            cardImage = this.GetComponent<Image>();
        }
        private void Grayscale(bool enableGrayscale)
        {
            if (enableGrayscale == true)
            {
                selectCard.image.material = grayscaleMaterial;
                cardImage.material = grayscaleMaterial;
            }
            else
            {
                selectCard.image.material = deafultMaterial;
                cardImage.material = deafultMaterial;
            }
        }
        private void GrayscaleCards()
        {
            if (manaRefil.setManaCounter < entity.cost)
            {
                enableGrayscale = true;
            }
            else enableGrayscale = false;
            Grayscale(enableGrayscale);
        }
        private void FixedUpdate()
        {
            GrayscaleCards();
        }
    }
}