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
            if(rewardsData.trophies >= 0) levels[0].interactable = true;
                else levels[0].interactable = false;
            if(rewardsData.trophies >= 100) levels[1].interactable = true;
                else levels[1].interactable = false;
            if(rewardsData.trophies >= 300) levels[2].interactable = true;
                else levels[2].interactable = false;
            if(rewardsData.trophies >= 500) levels[3].interactable = true;
                else levels[3].interactable = false;
        }
    }
}