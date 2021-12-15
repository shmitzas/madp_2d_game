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

        public UnityAction<EntityEvents> OnDoDamage;

        [HideInInspector] public EntityEvents target;

        [HideInInspector] public float timeToActNext = 0f;
        [HideInInspector] public HealthBar healthBar;
        [HideInInspector] public float healthRemaining;
        [HideInInspector] public float damage;
        [HideInInspector] public float attackRatio;
        [HideInInspector] public float attackRange;

        [HideInInspector] public float lastAttackTime = -1000f;

        [HideInInspector] public float timeNextStep = 0f;
        public void SetTarget(EntityEvents t)
        {
            target = t;
            t.OnDie += TargetIsDead;
        }
        public void Seek()
        {
            state = States.Seeking;

        }
        public void StartFighting()
        {
            state = States.Fighting;
        }
        public void DoDamage()
        {
            lastAttackTime = Time.time;
            if(OnDoDamage != null)
				OnDoDamage(this);
        }
        public void Stop()
        {
            state = States.Idle;
        }
        public void Die()
        {
            state = States.Dead;
            if (OnDie != null)
                OnDie(this);
        }
        public bool TargetInRange()
        {   
            return (transform.position-target.transform.position).sqrMagnitude <= attackRange*attackRange;
        }
        protected void TargetIsDead(EntityEnums p)
        {
            state = States.Idle;

            target.OnDie -= TargetIsDead;

            timeNextStep = lastAttackTime;

        }
        public float SufferDamage(float damage)
        {
            healthRemaining -= damage;
            if (healthRemaining <= 0f && state != States.Dead)
            {
                Die();
            }

            return healthRemaining;
        }
    }
}