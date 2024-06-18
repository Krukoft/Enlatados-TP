using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    List<GameObject> players = new List<GameObject>(); // Lista para almacenar todos los jugadores
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    Vector3 despoint;
    bool walkpointSet;
    [SerializeField] float range;
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;
    //hola

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player"); // Busca todos los GameObjects con la etiqueta "Player"
        foreach (GameObject playerObj in playerObjects)
        {
            players.Add(playerObj); // Agrega cada jugador encontrado a la lista
        }
    }

    void Update()
    {
        UpdatePlayersList();

        if (players.Count == 0)
            return; // Si no hay jugadores, salir del método Update()

        UpdatePlayerStatus();

        if (!playerInSight && !playerInAttackRange)
            Patrol();
        else if (playerInSight && !playerInAttackRange)
            Chase();
        else if (playerInSight && playerInAttackRange)
            Attack();
    }

    void UpdatePlayersList()
    {
        players.RemoveAll(player => player == null);
    }

    void UpdatePlayerStatus()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    }

    void Attack()
    {
        // Implementar la lógica de ataque del enemigo aquí
    }

    void Chase()
    {
        GameObject closestPlayer = GetClosestPlayer();

        if (closestPlayer != null)
            agent.SetDestination(closestPlayer.transform.position);
    }

    GameObject GetClosestPlayer()
    {
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 enemyPosition = transform.position;

        foreach (GameObject player in players)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(enemyPosition, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = player;
                }
            }
        }

        return closest;
    }

    void Patrol()
    {
        if (!walkpointSet)
            SearchForDest();

        if (walkpointSet)
            agent.SetDestination(despoint);

        if (Vector3.Distance(transform.position, despoint) < 10)
            walkpointSet = false;
    }

    void SearchForDest()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, 1);

        despoint = hit.position;
        walkpointSet = true;
    }
}