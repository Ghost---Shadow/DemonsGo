using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Slider healthBar;
    public Spell spellHandle;
    public int currentSpell;

    private float health;   

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
        Debug.Log("Player death");
        health = healthBar.maxValue;
        healthBar.value = healthBar.maxValue;
    }

    public void setCurrentSpell(int spell){
        //Debug.Log(spell);
        currentSpell = spell;
        spellHandle.currentSpell = spell;
    }
}
