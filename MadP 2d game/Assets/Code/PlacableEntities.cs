using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MADP
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
    public class PlaceableEntities : ScriptableObject
    {
        [Header("Common")]
        public EntityData.Type EntityType;
        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        
        [Header("Units and Buildings")]
        public EntityData.EntityAttackType attackType = EntityData.EntityAttackType.Close;
        public EntityData.EntityTarget targetType = EntityData.EntityTarget.Both;
        public float attackRatio = 1f; //time between attacks
        public float damagePerAttack = 2f; //damage each attack deals
        public float attackRange = 1f;
        public float hitPoints = 200f; //when units or buildings suffer damage, they lose hitpoints
		public AudioClip attackClip, dieClip;

        [Header("Units")]
        public float speed = 5f; //movement speed
        
        [Header("Obstacles and Spells")]
        public float lifeTime = 5f; //the maximum lifetime of the Placeable. Especially important for obstacle types, so they are removed after a while
        
        [Header("Spells")]
        public float damagePerSecond = 1f; //damage per second for non-instantaneous spells
    }
}