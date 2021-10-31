using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace RushNDestroy
{
    public class EntityEnums : MonoBehaviour
    {
        public Type type;
        public enum Type
        {
            Unit,
            Structure
        }
        public enum TargetType
        {
            Character,
            Structure,
            Any
        }
    }
}
