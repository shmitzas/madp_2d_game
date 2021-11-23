using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{

    public class BuildingData : EntityEvents
    {
        public void Activate(Faction pFaction, EntityData pData)
        {
            entityType = pData.entityType;
            faction = pFaction;
            healthRemaining = pData.health;
            targetType = pData.targetType;
            //TODO: add more as necessary

        }

        protected override void Die()
        {
            base.Die();
            //audioSource.PlayOneShot(dieAudioClip, 1f);

            //Debug.Log("Building is dead", gameObject);
        }
    }
}