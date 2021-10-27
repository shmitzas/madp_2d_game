using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RushNDestroy
{
    [CreateAssetMenu(fileName = "New Placeable", menuName = "RushNDestroy/Placeable Data")]
    public class PlaceablesData : ScriptableObject
    {
        [Header("Common")]
        public Placeable.PlaceableType pType;
        public GameObject associatedPrefab;
        public GameObject alternatePrefab;
        
        [Header("Units and Buildings")]
        public ThinkingPlaceable.AttackType attackType = ThinkingPlaceable.AttackType.Melee;
        public Placeable.PlaceableTarget targetType = Placeable.PlaceableTarget.Both;
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
        public float damagePerSecond = 1f;
    }
}