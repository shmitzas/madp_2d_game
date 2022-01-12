using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    public class BuildingData : EntityEvents
    {
        [Header("This will affect only the towers")]
        public Sprite destroyedTower;
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
            sr = GetComponent<SpriteRenderer>();
            towerDead = destroyedTower;
            targetAirborneEntities = entity.targetAirborneEntities;

            state = States.Idle;
        }

        private void Update()
        {
            switch (state)
            {
                case States.SeekingUnit:
                    if (target == null)
                        return;
                    base.SeekUnit();
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
