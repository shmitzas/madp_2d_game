using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RushNDestroy
{
    public class GameOverMenu : MonoBehaviour
    {
        public Text coinsAmount;
        public Text trophiesAmount;
        public GameObject rewardsMenu;
        public GameObject tieText;

        public void UpdateGameOverMenu(int coins, int trophies, int gameWon)
        {
            if (gameWon == 2)
            {
                coinsAmount.text = "+" + coins;
                trophiesAmount.text = "+" + trophies;
                rewardsMenu.SetActive(true);
                tieText.SetActive(false);
            }
            else if (gameWon == 1)
            {
                coinsAmount.text = "-" + coins;
                trophiesAmount.text = "-" + trophies;
                rewardsMenu.SetActive(true);
                tieText.SetActive(false);
            }
            else{
                rewardsMenu.SetActive(false);
                tieText.SetActive(true);
            }
        }
    }
}
