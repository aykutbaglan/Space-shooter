using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth = 200;
    public int currentHealth;
    public Slider healthBar;
    public EnemyShipController enemyShipController;

    private void Start()
    {
        healthBar.gameObject.SetActive(true);
        ResetHealth();
    }
    public void SetMaxHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
    public void SetHealth(int health)
    {
        healthBar.value = health;
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            enemyShipController.EnemyDie();
        } 
    }
    public void ResetHealth()
    {
        currentHealth = enemyHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = enemyHealth;
            healthBar.value = enemyHealth;
        }
    }
}