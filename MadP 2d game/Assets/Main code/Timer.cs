using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RushNDestroy
{
    public class Timer : MonoBehaviour
    {
        public ManaRefill manarefill;
        public float timeRemaining;
        public bool timerIsRunning;
        private Text timeText;
        public GameManager gameManager;

        private void Start()
        {
            // Starts the timer automatically
            timeText = GetComponent<Text>();
            timerIsRunning = true;
        }

        private void FixedUpdate()
        {
            if (timerIsRunning)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);

                if (timeRemaining <= 60)
                {
                    manarefill.addMana = 0.025f;
                }
                if (timeRemaining <= 0)
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    timerIsRunning = false;
                    gameManager.gameOver = true;
                    gameManager.GameOver();
                }
            }
        }

        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}