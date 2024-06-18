using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy3 : MonoBehaviour
{
    List<GameObject> players = new List<GameObject>(); // Lista para almacenar todos los jugadores
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    Vector3 despoint;
    bool walkpointSet;
    [SerializeField] float range;
    [SerializeField] float sightRange, attackRange;
    [SerializeField] int damageAmount;
    [SerializeField] float attackCooldown; // Nuevo campo para el tiempo de enfriamiento entre ataques

    bool playerInSight, playerInAttackRange;
    float lastAttackTime; // Variable para controlar el último tiempo de ataque

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindAllPlayers();
        lastAttackTime = -attackCooldown; // Configura el tiempo de último ataque inicial a un valor que permita el primer ataque
    }

    void Update()
    {
        UpdatePlayersList();

        if (players.Count == 0 || !PlayerInVisionRange())
        {
            Patrol();
        }
        else
        {
            UpdatePlayerStatus();

            if (playerInSight && !playerInAttackRange)
            {
                Chase();
            }
            else if (playerInSight && playerInAttackRange)
            {
                if (Time.time - lastAttackTime >= attackCooldown) // Verifica el cooldown
                {
                    Attack();
                    lastAttackTime = Time.time; // Actualiza el tiempo del último ataque
                }
            }
        }
    }

    void FindAllPlayers()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObj in playerObjects)
        {
            players.Add(playerObj);
        }
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

    bool PlayerInVisionRange()
    {
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance <= sightRange)
                {
                    return true; // Devuelve verdadero si al menos un jugador está en el rango de visión
                }
            }
        }
        return false; // Si ningún jugador está en el rango de visión
    }

    void Attack()
    {
      if (playerInAttackRange)
        {
            // Implementa la lógica de ataque aquí
            // Puedes acceder al script de salud del jugador y reducir su vida
            // Por ejemplo, asumiendo que el jugador tiene un script PlayerHealth:
            PlayerHealth playerHealth = GetClosestPlayer()?.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Reduce la vida del jugador
            }
        }
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