using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RushNDestroy
{
    public class MenuManager : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }
        public void LoadGameMenu()
        {
            SceneManager.LoadScene(2);
        }
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
