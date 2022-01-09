using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace RushNDestroy
{
    public class MenuManager : MonoBehaviour
    {
        public void LoadLevel(int level)
        {
            SceneManager.LoadScene(level);
        }
        public void LoadGameMenu()
        {
            SceneManager.LoadScene(1);
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
