using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RushNDestroy
{
    public class EntityEvents : EntityEnums
    {
        [HideInInspector] public States state = States.Dragged;
        public enum States
        {
            Dragged,
            Idle,
            Seeking,
            Fighting,
            Dead
        }

        [HideInInspector] public EntityEvents target;

        [HideInInspector] public float timeToActNext = 0f;
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
        private void Start()
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
        public virtual void SetTarget(EntityEvents t)
        {
            target = t;
            t.OnDie += TargetIsDead;
        }

        public bool TargetInRange()
        {
            return (transform.position - target.transform.position).sqrMagnitude <= attackRange;
        }

        protected void TargetIsDead(EntityEnums p)
        {
            state = States.Idle;

            target.OnDie -= TargetIsDead;

        }

        public virtual void Seek()
        {
            state = States.Seeking;
        }

        public void SufferDamage(float damage)
        {
            healthRemaining -= damage;
            healthBar.SetHealth(healthRemaining);

            if (healthRemaining <= 0)
            {
                Destroy(this.gameObject);
                Die();
            }
        }

        public virtual void Stop()
        {
            state = States.Idle;
        }

        protected virtual void Die()
        {
            state = States.Dead;

            if (OnDie != null)
                OnDie(this);
        }
    }
}