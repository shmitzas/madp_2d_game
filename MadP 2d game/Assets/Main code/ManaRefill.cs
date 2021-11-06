using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RushNDestroy
{
    public class ManaRefill : MonoBehaviour
    {
        public Slider slider;
        public Text manaCounter;
        public float mana { get; set; }
        public float addMana;
        public int setManaCounter { get; private set; }
        private void FixedUpdate()
        {
            UpdateMana();
        }

        private void UpdateMana()
        {
            mana += addMana;
            if (mana > 10f) mana = 10f;
            slider.value = mana;
            setManaCounter = (int)mana;
            UpdateManaCounter(setManaCounter);
            // if(Input.GetKeyDown(KeyCode.Space)) if(mana>=2) mana-=2;
        }

        private void UpdateManaCounter(int setManaCounter)
        {
            switch (setManaCounter)
            {
                case 1:
                    manaCounter.text = "1";
                    break;
                case 2:
                    manaCounter.text = "2";
                    // allowSpawnWarrior = true;
                    break;
                case 3:
                    manaCounter.text = "3";
                    break;
                case 4:
                    manaCounter.text = "4";
                    break;
                case 5:
                    manaCounter.text = "5";
                    break;
                case 6:
                    manaCounter.text = "6";
                    break;
                case 7:
                    manaCounter.text = "7";
                    break;
                case 8:
                    manaCounter.text = "8";
                    break;
                case 9:
                    manaCounter.text = "9";
                    break;
                case 10:
                    manaCounter.text = "10";
                    break;

                default:
                    manaCounter.text = "0";
                    break;
            }
        }
    }
}