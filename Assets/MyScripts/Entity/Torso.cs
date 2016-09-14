using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Torso : MonoBehaviour
{
    public GameObject[] torsos;
    public float[] damages;
    public float[] counterWindow;
    public Text _attackDirection;
    public PlayerController player;

    private GameObject currentTorso;
    private int index;

    private SwipeDetector.SwipeDirection attackDirection;
    private SwipeDetector.SwipeDirection defendDirection;
    private bool hasDefended;

    void Start()
    {
        currentTorso = null;
    }

    void OnEnable()
    {
        SwipeDetector.Swipe += counterAttack;
    }


    void OnDisable()
    {
        SwipeDetector.Swipe -= counterAttack;
    }

    public void spawnRandomTorso()
    {
        if (currentTorso != null)
        {
            DestroyImmediate(currentTorso);
        }
        index = (int)Random.Range(0, torsos.Length);
        currentTorso = (GameObject)Instantiate(torsos[index], transform.position, Quaternion.identity);
        currentTorso.transform.parent = this.transform;
    }

    public void attack()
    {
        attackDirection = (SwipeDetector.SwipeDirection)((int)Random.Range(0, 4));
        //Debug.Log("Attacking from "+direction);
        _attackDirection.text = "Attacking from " + attackDirection;
        StartCoroutine("waitForCounter");
    }

    private IEnumerator waitForCounter()
    {
        hasDefended = false;
        yield return new WaitForSeconds(counterWindow[index]);
        _attackDirection.text = "";
        if (attackDirection != defendDirection || !hasDefended)
        {
            player.damage(damages[index]);
        }
        else
        {
            _attackDirection.text = "Good job";
        }
    }

    public void counterAttack(SwipeDetector.SwipeDirection direction)
    {
        //Debug.Log(attackDirection + " " + defendDirection);
        defendDirection = direction;
        hasDefended = true;
    }
}
