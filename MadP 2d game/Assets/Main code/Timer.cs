using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RushNDestroy
{
public class Timer : MonoBehaviour
{   
    public ManaRefill manarefill;
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
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
            if (timeRemaining > 120)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            else if (timeRemaining <120 && timeRemaining >0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                manarefill.addMana = 0.025f;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                gameManager.gameOver = true;
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