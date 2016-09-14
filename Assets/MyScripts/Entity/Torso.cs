using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Torso : MonoBehaviour
{
    public GameObject[] torsos;
    public float[] damages;
    public float[] counterWindow;

    public int index;

    public Text _attackDirection;

    private GameObject currentTorso;
    private SwipeDetector.SwipeDirection attackDirection;
    private SwipeDetector.SwipeDirection defendDirection;
    private bool hasDefended;
    private Entity entity;

    void Start()
    {
        currentTorso = null;
        entity = transform.parent.gameObject.GetComponent<Entity>();
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

        if (attackDirection != defendDirection || !hasDefended)
        {
            entity.player.damage(damages[index]);
            _attackDirection.text = "Swipe " + attackDirection + " to defend";
        }
        else
        {
            _attackDirection.text = "Good job! Now tap to attack";
        }
    }

    public void counterAttack(SwipeDetector.SwipeDirection direction)
    {
        //Debug.Log(attackDirection + " " + defendDirection);
        defendDirection = direction;
        hasDefended = true;
    }

    public bool isStaggered()
    {
        return hasDefended;
    }
}
