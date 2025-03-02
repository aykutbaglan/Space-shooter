using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    public int playerHealt = 100;
    public int currentHealt;

    public Slider healtBar;
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
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealt <= 0) return; // Eðer zaten ölü ise, tekrar iþlem yapma

        currentHealt -= damage;
        if (healtBar != null)
        {
            healtBar.value = currentHealt;
        }

        Debug.Log("Player Health: " + currentHealt);

        if (currentHealt <= 0)
        {
            playerController.PlayerDie();
        }
    }
}