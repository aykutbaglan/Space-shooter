using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealt = 100;
    public int currentHealt;

    public Slider healtBar;
    public Gradient gradient;
    public Image fill;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        currentHealt = playerHealt;
    }

    private void Start()
    {
        if (healtBar != null)
        {
            healtBar.maxValue = playerHealt;
            healtBar.value = currentHealt;

            fill.color = gradient.Evaluate(1f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealt <= 0) return; // Eðer zaten ölü ise, tekrar iþlem yapma

        currentHealt -= damage;
        if (healtBar != null)
        {
            healtBar.value = currentHealt;
            fill.color = gradient.Evaluate(healtBar.normalizedValue);
        }

        Debug.Log("Player Health: " + currentHealt);

        if (currentHealt <= 0)
        {
            playerController.PlayerDie();
        }
    }
    public void ResetHealth()
    {
        currentHealt = playerHealt;
        if (healtBar != null)
        {
            healtBar.value = currentHealt;
            fill.color = gradient.Evaluate(1f);
        }
    }
}