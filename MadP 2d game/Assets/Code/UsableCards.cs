using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableCards : MonoBehaviour
{
    private bool enableGrayscale = true;
    public Material deafultMaterial;
    public Material grayscaleMaterial;
    public Button selectCard;
    public Image cardImage;
    public ManaRefil manaRefil;
    public entities_data entity;

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
            Debug.Log(selectCard.gameObject.name);
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
