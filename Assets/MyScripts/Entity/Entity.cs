using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Entity : MonoBehaviour
{
    public Head head;
    public Torso torso;

    public Slider healthBar;

    private float health;

    public void respawn()
    {
        head.spawnRandomHead();
        torso.spawnRandomTorso();
    }

    public void damage(float amt)
    {
        health -= amt;
        health = health < 0 ? 0 : health;
        healthBar.value = health;
        if (health == 0)
            death();
    }

    public void death()
    {
        Debug.Log("Died");
        respawn();
    }
}
