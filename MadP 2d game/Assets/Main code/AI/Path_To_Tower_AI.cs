// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Pathfinding;

// public class Path_To_Tower_AI : MonoBehaviour
// {
//     public Transform target;
//     public float speed = 20f;
//     public float nextWaypointDistance = 1f;
//     public float range = 2f;
//     Path path;
//     int currentWaypoint = 0;
//     bool reachedEndOfPath = false;
//     Seeker seeker;
//     public Rigidbody2D rb;

//     void Start()
//     {
//         seeker = GetComponent<Seeker>();
//         rb = GetComponent<Rigidbody2D>();
//         InvokeRepeating("UpdatePath", 0f, 0.5f);
//     }

//     void UpdatePath()
//     {
//         seeker.StartPath(rb.position, target.position, OnPathComplete);
//     }

//     void OnPathComplete(Path p)
//     {
//         if (!p.error)
//         {
//             path = p;
//             currentWaypoint = 0;
//         }
//     }
//     void FixedUpdate()
//     {
//         if (path == null)
//         {
//             return;
//         }
//         if (currentWaypoint >= path.vectorPath.Count)
//         {
//             reachedEndOfPath = true;
//             return;
//         }
//         else
//         {
//             reachedEndOfPath = true;
//         }
        
        
//         Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
//         rb.velocity = direction * speed;
        

//         float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
//         if (distance < nextWaypointDistance)
//         {
//             currentWaypoint++;
//         }
        

//     }
    
// }
