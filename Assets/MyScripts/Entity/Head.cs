using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour
{
    public GameObject[] heads;

    private GameObject currentHead;
	private int index;
    void Start()
    {
        currentHead = null;
    }

    public void spawnRandomHead()
    {
        if (currentHead != null)
        {
            DestroyImmediate(currentHead);
        }
        index = (int)Random.Range(0, heads.Length);
        currentHead = (GameObject)Instantiate(heads[index], transform.position, Quaternion.identity);
        currentHead.transform.parent = this.transform;
    }
}