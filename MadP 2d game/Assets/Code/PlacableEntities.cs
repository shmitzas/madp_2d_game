using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MADP
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
    public class PlacableEntities : ScriptableObject
    {
        [Header("Common")]
        public EntityData.Type EntityType;
        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        public Sprite artwork;

        [Header("Units and Buildings")]
        public EntityData.EntityAttackType attackType = EntityData.EntityAttackType.Close;
        public EntityData.EntityTarget targetType = EntityData.EntityTarget.Both;
        public float attackRatio = 1f; //time between attacks
        public float attackDamage = 2f; //damage each attack deals
        public float attackRange = 1f;
        public int health = 200; //when units or buildings suffer damage, they lose hitpoints
        public int cost = 0;

        [Header("Units")]
        public float speed = 5f; //movement speed

        [Header("Obstacles and Spells")]
        public float lifeTime = 5f; //the maximum lifetime of the Placeable. Especially important for obstacle types, so they are removed after a while

        [Header("Spells")]
        public float damagePerSecond = 1f; //damage per second for non-instantaneous spells
    }
}