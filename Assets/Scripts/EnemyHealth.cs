using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima del enemigo
    private int currentHealth; // Vida actual del enemigo

    public Slider healthSlider; // Referencia al slider de vida del enemigo

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la vida actual al máximo al inicio
        UpdateHealthUI(); // Actualiza el Slider de vida al iniciar el juego
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

    void Die()
    {
        // Lógica para cuando el enemigo muere
        gameObject.SetActive(false); // Desactiva el GameObject del enemigo en este ejemplo
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // Establece el valor máximo del Slider
            healthSlider.value = currentHealth; // Actualiza el valor actual del Slider de vida del enemigo
        }
    }
}