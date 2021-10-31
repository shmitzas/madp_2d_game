using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushNDestroy
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
    public class EntityData : ScriptableObject
    {
        [Header("Prefab, artwork")]
        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        public Sprite artwork;

        [Header("Data")]
        public EntityEnums.Type entityType;
        public EntityEvents.AttackType attackType = EntityEvents.AttackType.Close;
        public EntityEnums.TargetType targetType = EntityEnums.TargetType.Any;
        public float attackRatio = 1f; //time between attacks
        public int attackDamage = 2; //damage each attack deals
        public float attackRange = 1f;
        public int health = 200; //when units or buildings suffer damage, they lose hitpoints
        public int cost = 0;
        public float speed = 2f; //movement speed

        /* ---- Spells (if we decide to add them)
        [Header("Obstacles and Spells")]
        public float lifeTime = 5f; //the maximum lifetime of the Placeable. Especially important for obstacle types, so they are removed after a while

        [Header("Spells")]
        public float damagePerSecond = 1f; //damage per second for non-instantaneous spells
        */
    }
}