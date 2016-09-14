using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour
{
    public GameObject[] heads;
    public int index = 0;

    private GameObject currentHead;
    private Entity entity;

    void Start()
    {
        currentHead = null;
        entity = transform.parent.gameObject.GetComponent<Entity>();
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