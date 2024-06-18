using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    Vector3 despoint;
    bool walkpointSet;
    [SerializeField] float range;
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;


    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    } 

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(!playerInSight && !playerInAttackRange)Patrol();
        if(playerInSight && !playerInAttackRange)Chase();
        if(playerInSight && playerInAttackRange)Attack();
    }

    void Attack()
    {
        
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }


    void Patrol()
    {
        if (!walkpointSet) SearchForDest();
        if (walkpointSet) agent.SetDestination(despoint);
        if (Vector3.Distance(transform.position, despoint) < 10) walkpointSet = false;
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        despoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(despoint, Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }
}