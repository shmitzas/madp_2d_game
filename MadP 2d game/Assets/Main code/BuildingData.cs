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
            attackRange = entity.attackRange;
            damage = entity.attackDamage;
            attackRatio = entity.attackRatio;
            healthBar = GetComponentInChildren<HealthBar>();
            healthBar.StartHealthBar(healthRemaining);

            state = States.Idle;
        }

        private void Update() {
            switch (state)
            {
                case States.Seeking:
                    if (target == null)
                        return;
                    base.Seek();
                    break;
                case States.Fighting:
                    base.Stop();
                    base.StartFighting();
                    break;
                case States.Dead:
                    base.Die();
                    break;
            }
        }
    }
}
