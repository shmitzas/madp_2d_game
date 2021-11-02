using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    public class EntityEvents : EntityEnums
    {
        [HideInInspector] public EntityEvents target;
        [HideInInspector] public HealthBar healthBar;
        [HideInInspector] public float healthRemaining;
        [HideInInspector] public int damage;
        [HideInInspector] public float attackRatio;
        [HideInInspector] public float attackRange;
        [HideInInspector] public AttackType attackType;
        public enum AttackType
        {
            Close,
            Ranged
        }
        private void Awake()
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }

        public void SufferDamage(int damage)
        {
            healthRemaining -= damage;
            healthBar.SetHealth(healthRemaining);

            if (healthRemaining <= 0)
                Die();
        }
        public void Die()
        {
            Destroy(this.gameObject);
        }
    }
}