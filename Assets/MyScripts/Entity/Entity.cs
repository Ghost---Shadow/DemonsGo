using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Entity : MonoBehaviour
{
    public Head head;
    public Torso torso;

    public Slider healthBar;
	public float attackDelay = 1f;

	public PlayerController player;

    private float health;
	private bool isDead = false;

    public void respawn()
    {
        head.spawnRandomHead();
        torso.spawnRandomTorso();
		StartCoroutine("keepAttacking");
    }	

	IEnumerator keepAttacking(){
		while(!isDead){
			torso.attack(player);
			yield return new WaitForSeconds(attackDelay);
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

    public void death()
    {
        Debug.Log("Died");
        respawn();
    }
}
