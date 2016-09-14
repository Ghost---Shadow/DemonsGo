using UnityEngine;
using System.Collections;

public class EntitySpawner : MonoBehaviour
{
    public Entity entity;

	void Start(){
		spawnEntity();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            spawnEntity();
        }
    }

    public void spawnEntity()
    {
        entity.respawn();
    }
}
