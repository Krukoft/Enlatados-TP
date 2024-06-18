using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthScript : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima
    public int currentHealth; // Salud actual

    void Start()
    {
        currentHealth = maxHealth; // Establecer la salud actual a la salud máxima al inicio
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reducir la salud actual por la cantidad de daño recibido

        if (currentHealth <= 0)
        {
            Die(); // Si la salud llega a cero o menos, llama al método Die()
        }
    }

    void Die()
    {
        // Aquí puedes agregar lógica para lo que sucede cuando el personaje muere
        // Por ejemplo, puedes desactivar el GameObject, reproducir una animación de muerte, reiniciar el nivel, etc.
        gameObject.SetActive(false); // En este ejemplo, se desactiva el GameObject
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth); // Añadir salud sin exceder el máximo
    }

    public float GetCurrentHealth()
    {
        return currentHealth; // Obtener la salud actual
    }

    public float GetMaxHealth()
    {
        return maxHealth; // Obtener la salud máxima
    }
}