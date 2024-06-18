using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackInterval = 1.5f; // Intervalo de ataque del enemigo
    public int damagePerAttack = 10; // Daño por cada ataque del enemigo
    public float attackRange = 2f; // Rango de ataque del enemigo
    private PlayerHealth playerHealth; // Referencia al script PlayerHealth del jugador

    private float nextAttackTime;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackInterval;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damagePerAttack); // Aplica daño al jugador
                    }
                }
            }
        }
    }
}