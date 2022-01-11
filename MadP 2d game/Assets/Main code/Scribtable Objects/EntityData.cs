using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Rush N Destroy/Entity")]
    public class EntityData : ScriptableObject
    {
        [Header("Prefab, artwork")]
        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        public Sprite artwork;

        [Header("Data")]
        public EntityEnums.Type entityType;
        public EntityEnums.ExtraType extraType = EntityEnums.ExtraType.None;
        public EntityEnums.TargetType targetType = EntityEnums.TargetType.All;
        public float attackRatio; //time between attacks
        public float attackDamage; //damage each attack deals
        public float attackRange;
        public float health; //when units or buildings suffer damage, they lose hitpoints
        public float cost;
        public float speed; //movement speed
        public bool targetAirborneEntities;

        [Header("Upgrades")]
        public int upgradeLevel;
        public int upgradeCost;
        public bool owned = false;
        public int buyCost;
    }
    [System.Serializable]
    public class EntityDataToClassList
    {
        public List<EntityDataToClass> list;
    }
    [System.Serializable]
    public class EntityDataToClass
    {
        public float attackRatio;
        public float attackDamage;
        public float health;
        public float speed;
        public int upgradeLevel;
        public int upgradeCost;
        public bool owned;
        public int buyCost;
        public EntityDataToClass(EntityData entity)
        {
            attackRatio = entity.attackRatio;
            attackDamage = entity.attackDamage;
            health = entity.health;
            speed = entity.speed;
            upgradeLevel = entity.upgradeLevel;
            upgradeCost = entity.upgradeCost;
            owned = entity.owned;
            buyCost = entity.buyCost;
        }
    }

}