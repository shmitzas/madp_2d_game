using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entities_data : MonoBehaviour
{
    public string entityName;
    public string type;
    public int health;
    public float speed;
    public int attackDamage;
    public float attackRange;
    public int cost;

    private void LateUpdate()
    {
        TakeDamage();
    }
    private void TakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.Space)) health -= 6;
        //Once entity dies, it should be removed from scene, but should remain spawnable
        if (health <= 0)
        {
            Debug.Log("u ded");
            //this.gameObject.SetActive(false);
        }
    }
}
