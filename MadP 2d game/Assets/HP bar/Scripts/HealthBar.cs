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
        private Transform transformToFollow;

        public void StartHealthBar(EntityEvents entity)
        {
            slider.maxValue = entity.healthRemaining;
            slider.value = entity.healthRemaining;
            fill.color = gradient.Evaluate(1f); //set this color at max
        }
        public void SetHealth(float health)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue); //color and value depends, so we use normalized(ussualy from zero to 1)
        }
        public void Move()
        {
            if(transformToFollow != null)
                transform.position = transformToFollow.position;
        }
    }
}