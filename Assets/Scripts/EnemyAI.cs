using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    void Update()
    {
        // Move towards the player's position every frame
        agent.SetDestination(player.position);
    }
}
