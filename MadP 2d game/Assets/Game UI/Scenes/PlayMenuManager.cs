using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace RushNDestroy
{
    public class PlayMenuManager : MonoBehaviour
    {
        public Button continueButton;
        private void Awake()
        {
            continueButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
            if(Directory.Exists(Application.persistentDataPath + "/saves"))
                continueButton.gameObject.SetActive(true);
        }
    }
}