using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RushNDestroy
{
    public class UnitData : EntityEvents
    {
        private float speed;
        
        private NavMeshAgent agent;

        void Start()
        {
            agent.updateRotation = false; //disables 3d rotation cause this is 2d game 
            agent.updateUpAxis = false;
        }

        public void Awake()
        {
            entityType = EntityEnums.Type.Unit;

            agent = GetComponent<NavMeshAgent>(); //disabled until Activate() is called
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
            healthBar = GetComponentInChildren<HealthBar>();
            healthBar.StartHealthBar(healthRemaining);

            agent.speed = speed;
            //Debug.Log(agent.speed + " DA SPED");

            state = States.Idle;
            agent.enabled = true;
            agent.stoppingDistance = attackRange/2;
            agent.acceleration = speed*10;
        }
        private void Update()
        {
            switch (state)
            {
                case States.SeekingUnit:
                    if (target == null)
                        return;
                    base.SeekUnit();
                    agent.SetDestination(target.transform.position);
                    agent.isStopped = false;
                    break;
                case States.SeekingTower:
                    if (target == null)
                        return;
                    base.SeekTower();
                    agent.SetDestination(target.transform.position);
                    agent.isStopped = false;
                    break;
                case States.Fighting:
                    base.Stop();
                    agent.isStopped = true;
                    base.StartFighting();
                    break;
                case States.Dead:
                    base.Die();
                    agent.enabled = false;
                    break;
            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if(entityType == EntityEnums.Type.Unit)
                if(other.tag == "BrokenBridge") agent.speed *= 0.2f;
                else if(other.tag == "ShatteredBridge") agent.speed *= 0.5f;
        }
        private void OnTriggerExit2D(Collider2D other) {
            if(entityType == EntityEnums.Type.Unit)
                if(other.tag == "BrokenBridge") agent.speed = speed;
                else if(other.tag == "ShatteredBridge") agent.speed = speed;
        }
    }
}