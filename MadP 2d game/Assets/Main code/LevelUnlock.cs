using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RushNDestroy
{
    public class LevelUnlock : MonoBehaviour
    {
        public RewardsData rewardsData;
        public List<Button> levels;

        private void Start()
        {
            for(int i=0; i<levels.Count; i++)
            {
                Debug.Log(i);
                if(rewardsData.trophies>=0 && i==0) 
                    levels[i].interactable = true;
                    else levels[i].interactable = false;
                if(rewardsData.trophies>=100 && i==1) 
                    levels[i].interactable = true;
                    else levels[i].interactable = false;
                if(rewardsData.trophies>=300 && i==2) 
                    levels[i].interactable = true;
                    else levels[i].interactable = false;
                if(rewardsData.trophies>=500 && i==3) 
                    levels[i].interactable = true;
                    else levels[i].interactable = false;
            }
        }
    }
}