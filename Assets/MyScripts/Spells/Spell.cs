using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour
{
	public ParticleSystem[] spells;
    public bool isPlayerControlled = false;
	public float spellLength = 1.0f;
    public int currentSpell = 0;

    void OnEnable()
    {
        if(isPlayerControlled)
            Entity.Click += shoot;
    }

    void OnDisable()
    {
        if(isPlayerControlled)
            Entity.Click -= shoot;
    }

	public void shoot(){
		StartCoroutine(playParticles());
	}

	IEnumerator playParticles(){
		spells[currentSpell].Play();
		yield return new WaitForSeconds(spellLength);
		spells[currentSpell].Stop();
	}

    // Placeholder
    public static float getDamageTakenBy(int type, int spell)
    {
        return type == spell ? .1f : .2f;
    }
}
