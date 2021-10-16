using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public entities_data entity;

    public int maxHealth;
    public int currentHealth;


    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = this.entity.health;
        currentHealth = maxHealth;
    }

   void Update() //a function to take damage at every space.button press
    {
       if (Input.GetKeyDown(KeyCode.Space))
       {
           TakeDamage(20);
       }
   }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Traps")
        {
            Debug.Log("Player Collided With Traps");
        }
         else if (other.gameObject.tag == "Healing")
        {
            Debug.Log("Player Collided With Healing");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
       // Debug.Log("Damage Taken");

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
           // Debug.Log("Player Destroyed");
        }

        healthBar.SetHealth(currentHealth);
    }

    public void TakeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }
}
