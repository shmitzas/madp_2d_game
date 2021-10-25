using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MADP
{
    public class DamageOnCollision : MonoBehaviour
    {
        private int damage = 10;

        private void OnCollisionEnter2D(Collision2D other)
        {
            DamageEnemyPL(other);
            Debug.Log("TrapsTriggered");
        }

        public void DamageEnemyPL(Collision2D other)
        {
            if (other.transform.tag == "Player")
            {
                other.transform.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
}