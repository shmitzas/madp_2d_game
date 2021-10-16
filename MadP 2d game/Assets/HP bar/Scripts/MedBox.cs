using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBox : MonoBehaviour
{
    private int improve = 15;

    private void OnCollisionEnter2D(Collision2D other)
    {
        GiveHealth(other);
        Debug.Log("MedBoxTriggered");
    }

    public void GiveHealth(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponent<PlayerHealth>().TakeHealth(improve);
        }
    }
}
