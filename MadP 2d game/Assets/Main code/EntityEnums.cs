using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RushNDestroy
{
    public class EntityEnums : MonoBehaviour
    {
        [HideInInspector] public Faction faction;
        [HideInInspector] public TargetType target;
        public Type type;
        public enum Type
        {
            Unit,
            Structure
        }
        public enum TargetType
        {
            Ground,
            Structure,
            Any
        }
        public enum Faction{
            Player,
            Enemy
        }
    }
}
