using UnityEngine;
using UnityEngine.Events;

namespace MADP
{
    public class EntityData:MonoBehaviour
    {
        public enum Type
        {
            Character,
            Structure
        }

        public enum EntityAttackType
        {
            Close,
            Ranged,
            Air
        }
        public enum EntityTarget
        {
            OnlyBuildings,
            Both,
            None
        }
    }
}