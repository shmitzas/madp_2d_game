using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RushNDestroy
{
    public class Placeable : MonoBehaviour
    {
        public PlaceableType pType;
		
        [HideInInspector] public Faction faction;
        [HideInInspector] public PlaceableTarget targetType; //TODO: move to ThinkingPlaceable?
		[HideInInspector] public AudioClip dieAudioClip;

        public UnityAction<Placeable> OnDie;

        public enum PlaceableType
        {
            Unit,
            Obstacle,
            Building,
            Spell,
            Castle, //special type of building
        }

        public enum PlaceableTarget
        {
            OnlyBuildings,
            Both,
            None,
        }

        public enum Faction
        {
            Player, //Red
            Opponent, //Blue
            None,
        }
    }
}