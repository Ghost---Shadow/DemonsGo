using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour
{
	public ParticleSystem[] spells;
	public PlayerController player;
	public float spellLength = 1.0f;

    void OnEnable()
    {
        Entity.Click += shoot;
    }

    void OnDisable()
    {
        Entity.Click -= shoot;
    }

	public void shoot(){
		StartCoroutine(playParticles());
	}

	IEnumerator playParticles(){
		spells[player.currentSpell].Play();
		yield return new WaitForSeconds(spellLength);
		spells[player.currentSpell].Stop();
	}

    // Placeholder
    public static float getDamageTakenBy(int type, int spell)
    {
        return type == spell ? .1f : .2f;
    }
}
