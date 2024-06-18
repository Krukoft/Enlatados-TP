using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoShoot : MonoBehaviour
{
    public Transform firePoint; // Punto de origen del disparo
    public GameObject projectilePrefab; // Prefab del proyectil a disparar
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public float fireRate = 0.5f; // Cadencia de disparo
    public int shotsPerFire = 3; // Cantidad de disparos por ráfaga
    public float visionRange = 10f; // Rango de visión del jugador
    public float projectileLifetime = 3f; // Tiempo de vida del proyectil
    public int damagePerShot = 25; // Daño por cada bala

    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

     void Shoot()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange);

        int i = 0;
        GameObject projectile = null; // Asigna un valor por defecto

        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Enemy"))
            {
                GameObject enemy = hitColliders[i].gameObject;
                Vector3 fireDirection = enemy.transform.position - firePoint.position;

                for (int j = 0; j < shotsPerFire; j++)
                {
                    projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(fireDirection));
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.velocity = fireDirection.normalized * projectileSpeed;
                    }

                    Destroy(projectile, projectileLifetime); // Destruye la bala después de un tiempo
                }

                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot); // Aplica daño al enemigo
                }

                // Destruye el proyectil al impactar con el enemigo
                if (projectile != null)
                {
                    Destroy(projectile, 0.1f);
                }

                break; // Detener el bucle después de disparar al enemigo más cercano
            }
            i++;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}