using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Entity : MonoBehaviour
{
    public Head head;
    public Torso torso;

    public Slider healthBar;

    public PlayerController player;
    public float attackDelay = 2f;
    public float staggerDelay = 5f;

    private float health;
    private bool isDead = false;

    public delegate void ClickAction();
    public static event ClickAction Click;

    void OnEnable()
    {
        Click += hit;
    }

    void OnDisable()
    {
        Click -= hit;
    }

    public void clickedUpon(){
        Click();
    }

    public void respawn()
    {
        StopCoroutine("keepAttacking");
        health = 1.0f;
        head.spawnRandomHead();
        torso.spawnRandomTorso();
        StartCoroutine("keepAttacking");
    }

    IEnumerator keepAttacking()
    {
        while (!isDead)
        {
            torso.attack();
            float delay = torso.isStaggered() ? staggerDelay : attackDelay;
            yield return new WaitForSeconds(delay);
        }
        yield return null;
    }

    public void damage(float amt)
    {
        health -= amt;
        health = health < 0 ? 0 : health;
        healthBar.value = health;
        if (health == 0)
            death();
    }

    public void hit()
    {
        float damageTakenByTorso = Spell.getDamageTakenBy(torso.index, player.currentSpell);
        float damageTakenByHead = Spell.getDamageTakenBy(head.index, player.currentSpell);
        damage((damageTakenByTorso + damageTakenByHead) / 2.0f);
    }

    public void death()
    {
        Debug.Log("Died");
        respawn();
    }
}
