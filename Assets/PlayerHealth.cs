using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima del jugador
    private int currentHealth; // Vida actual del jugador

    public Slider healthSlider; // Referencia al slider de vida del jugador
    public AudioClip deathSound; // Variable para el audio de muerte
    private AudioSource audioSource; // Referencia al componente AudioSource

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la vida actual al máximo al inicio
        UpdateHealthUI(); // Actualiza el Slider de vida al iniciar el juego
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce la vida actual según el daño recibido

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die(); // Llama al método Die() si la vida llega a cero o menos
        }

        UpdateHealthUI(); // Actualiza el Slider de vida después de recibir daño
    }

   /* void Die()
    {
        // Lógica para cuando el jugador muere
        Debug.Log("El jugador ha muerto.");

        this.enabled = false; 
        //gameObject.SetActive(false);
        
        Destroy(gameObject);
    }*/

    void Die()
    {
        Debug.Log("El jugador ha muerto.");

        // Verificar si hay un clip de audio asignado y reproducirlo
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Destruir el GameObject del jugador después de un pequeño retraso
        Destroy(gameObject, 1.0f);
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // Establece el valor máximo del Slider
            healthSlider.value = currentHealth; // Actualiza el valor actual del Slider de vida del jugador
        }
    }
}