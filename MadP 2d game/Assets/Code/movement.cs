using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    Rigidbody2D body;

    float horizontal;
    float vertical;
    public entities_data entity;
   
    private float runSpeed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        runSpeed = entity.speed;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}

