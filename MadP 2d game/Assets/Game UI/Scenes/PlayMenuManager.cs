using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class PlayMenuManager : MonoBehaviour
    {
        public Button newGameButton;
        public Button continueButton;
        private bool firstTime = true;
        private void Awake()
        {
            if (firstTime)
                continueButton.gameObject.SetActive(true);
            else continueButton.gameObject.SetActive(false);

            newGameButton.onClick.AddListener(delegate { firstTime = false; });
        }
    }
}