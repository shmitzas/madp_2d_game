using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RushNDestroy
{
    public class EntityEvents : EntityEnums
    {
        [HideInInspector] public States state = States.Dragged;
        [HideInInspector] public Vector2 deathPos;
        public enum States
        {
            Dragged,
            Idle,
            SeekingTower,
            SeekingUnit,
            Fighting,
            Dead
        }

        public UnityAction<EntityEvents> OnDoDamage;
        public UnityAction<Vector2> CreateSmokeOnDeath;

        [HideInInspector] public EntityEvents target;

        [HideInInspector] public float timeToActNext = 0f;
        [HideInInspector] public HealthBar healthBar;
        [HideInInspector] public SpriteRenderer sr;
        [HideInInspector] public Sprite towerDead;
        [HideInInspector] public float healthRemaining;
        [HideInInspector] public float damage;
        [HideInInspector] public float attackRatio;
        [HideInInspector] public float attackRange;
        [HideInInspector] public bool targetAirborneEntities;

        [HideInInspector] public float detectRange = 1.2f;

        [HideInInspector] public float lastAttackTime = -1000f;

        [HideInInspector] public float timeNextStep = 0f;

        public void SetTarget(EntityEvents t)
        {
            target = t;
            t.OnDie += TargetIsDead;
        }
        public void SeekTower()
        {
            state = States.SeekingTower;
        }
        public void SeekUnit()
        {
            state = States.SeekingUnit;
        }
        public void StartFighting()
        {
            state = States.Fighting;
        }
        public void DoDamage()
        {
            lastAttackTime = Time.time;
            if (OnDoDamage != null)
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
            return (transform.position - target.transform.position).sqrMagnitude <= attackRange;
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
                if (this.entityType == EntityEnums.Type.Structure)
                {
                    sr.sprite = this.towerDead;
                    if (this.faction == EntityEnums.Faction.Enemy)
                        sr.flipX = true;
                    deathPos = this.gameObject.transform.position;
                    CreateSmokeOnDeath(deathPos);
                }
                Die();
            }

            return healthRemaining;
        }
    }
}