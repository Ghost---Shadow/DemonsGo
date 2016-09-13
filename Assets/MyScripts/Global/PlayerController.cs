using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Slider healthBar;

    private float health;
    public void damage(float amt)
    {
        health -= amt;
        health = health < 0 ? 0 : health;
        healthBar.value = health;
        if (health == 0)
            death();
    }

    void Update()
    {
    }

    public void death()
    {
        Debug.Log("Player death");
        health = healthBar.maxValue;
        healthBar.value = healthBar.maxValue;
    }
}
