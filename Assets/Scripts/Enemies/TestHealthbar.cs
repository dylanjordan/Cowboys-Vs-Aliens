using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHealthbar : MonoBehaviour
{
    public Slider Slider;

    public void SetHealth(float health, float maxHealth)
    {
        health = health / maxHealth;
        Debug.Log(health);
        Slider.value = health;

        Slider.fillRect.GetComponentInChildren<RawImage>().color = Color.Lerp(Color.red, Color.green, Slider.value);
    }
}
