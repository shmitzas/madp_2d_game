using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RushNDestroy
{
    public class BuildingData : EntityEvents
    {
        public void Activate(Faction pFaction, EntityData entity)
        {
            entityType = entity.entityType;
            faction = pFaction;
            healthRemaining = entity.health;
            targetType = entity.targetType;
        }
        protected override void Die()
        {
            base.Die();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SufferDamage(5);
        }
    }
}
