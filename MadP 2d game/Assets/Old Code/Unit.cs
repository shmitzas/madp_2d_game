using UnityEngine;

namespace MADP
{
    public class Unit : EntityData
    {
        private float speed;
        [HideInInspector] public EntityData target;
        [HideInInspector] public HealthBar healthBar;

        [HideInInspector] public int health;
        [HideInInspector] public float attackRange;
        [HideInInspector] public float attackRatio;
        [HideInInspector] public float lastBlowTime = -1000f;
        [HideInInspector] public float damage;
        private void Awake()
        {
            eType = EntityData.Type.Character;
        }

        public void Activate(PlacableEntities peData){
            health = peData.health;
            //target = peData.targetType;
            attackRange = peData.attackRange;
            attackRatio = peData.attackRatio;
            speed = peData.speed;
            damage = peData.attackDamage;
        }
    }
}

/*
            faction = pFaction;
            hitPoints = pData.hitPoints;
            targetType = pData.targetType;
            attackRange = pData.attackRange;
            attackRatio = pData.attackRatio;
            speed = pData.speed;
            damage = pData.damagePerAttack;
			attackAudioClip = pData.attackClip;
			dieAudioClip = pData.dieClip;
            //TODO: add more as necessary
*/