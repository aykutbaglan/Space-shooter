using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color[] colors;
    [SerializeField] private TMP_Text startCanvasTxt;
    [SerializeField] private TMP_Text endCanvasTxt;

    private int currentColorIndex = 0;
    public float colorChangeTime = 1.0f;
    private void Start()
    {
        StartCoroutine(ColorChangeRoutine());
    }

    private IEnumerator ColorChangeRoutine()
    {
        while (true)
        {
            if (colors.Length > 0)
            {
                startCanvasTxt.color = colors[currentColorIndex];
                endCanvasTxt.color = colors[currentColorIndex];

                currentColorIndex = (currentColorIndex + 1) % colors.Length;
            }
            yield return new WaitForSeconds(colorChangeTime);
        }
    }
}