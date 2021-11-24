using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RushNDestroy
{
    public class UnitData : EntityEvents
    {
        private float speed;
        private void Awake() {
            entityType = EntityEnums.Type.Unit;
        }
        public void Activate(Faction pFaction, EntityData entity)
        {
            faction = pFaction;  //for AI to know if this entity is friendly or enemy
            healthRemaining = entity.health;
            targetType = entity.targetType; //for AI to know what type of entity it can attack
            attackRange = entity.attackRange;
            attackRatio = entity.attackRatio;
            speed = entity.speed;
            damage = entity.attackDamage;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SufferDamage(5f);
        }
        protected override void Die()
        {
            base.Die();
        }
    }
}