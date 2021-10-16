using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public entities_data entity;

    public Gradient gradient; //set a color
    public Image fill;

    private void LateUpdate() {
        SetHealth(entity.health);
    }
    private void Start() {
        slider.maxValue = entity.health;
        slider.value = entity.health;
        fill.color = gradient.Evaluate(1f); //set this color at max
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue); //color and value depends, so we use normalized(ussualy from zero to 1)
    }
}
