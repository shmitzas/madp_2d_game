using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient; //set a color
        public Image fill;
        private float health;

        public void StartHealthBar(float health)
        {
            slider.maxValue = health;
            slider.value = health;
            fill.color = gradient.Evaluate(1f); //set this color at max
        }
        public void SetHealth(float health)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue); //color and value depends, so we use normalized(ussualy from zero to 1)
        }
    }
}