// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyAI : MonoBehaviour
// {
//     private enum State{
//         GoingToTower,
//         GoingToEnemy,
//         Fighting,
//         Dead,
//     }

// private Path_To_Tower_AI pathfindingMovement;
// private State state;
// private Transform player;
// private Transform tower;

// private void Awake(){
//     pathfindingMovement = GetComponent<Path_To_Tower_AI>();
//     state = State.GoingToTower;    
//      player = GameObject.FindWithTag("Player").transform;
//      tower = GameObject.FindWithTag("Tower").transform;
//     }

// private void Update(){
//     FindTarget();
// }

// private void FindTarget(){
//     float targetRange = 5f;
//     pathfindingMovement.target = tower;

//     if (Vector2.Distance(transform.position, player.position) > targetRange){
//         state = State.GoingToTower;
//     }   
//     else{
//             Debug.Log("I CAN SEE YOU AND I AM COMING FOR YOU");
//             pathfindingMovement.target = player;
//             state = State.GoingToEnemy;
//     }
    
// }

// private void OnCollisionEnter2D(Collision2D collision){  //should be implemented with range instead of collision
//             pathfindingMovement.target = tower;
//             Destroy(player.gameObject); //Should be switched to Fighting state
//             FindTarget();
//     }
// }