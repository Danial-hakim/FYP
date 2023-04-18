using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomTemplates : MonoBehaviour
{
	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime = 10;
	private bool builtNavMesh;
	public GameObject boss;
	void Update()
	{
		if (waitTime <= 0 && builtNavMesh == false)
		{
			for (int i = 0; i < rooms.Count; i++)
			{
				if (i == rooms.Count - 1)
				{
					buildMeshNow();
					builtNavMesh = true;
				}
			}
		}
		else
		{
			waitTime -= Time.deltaTime;
		}

		if(waitTime <= 0) { waitTime = 0; }
	}

	void buildMeshNow()
	{
        for (int i = 0; i < rooms.Count; i++)
		{
            rooms[i].GetComponent<NavMeshSurface>().BuildNavMesh();
		}
		rooms[0].GetComponent<EntitySpawner>().SpawnPlayer();
        for (int i = 1; i < rooms.Count; i++)
        {
			rooms[i].GetComponent<EntitySpawner>().SpawnHealZone();
			rooms[i].GetComponent<EntitySpawner>().SpawnEnemies();
			rooms[i].GetComponent<EntitySpawner>().SpawnBulletElements();
			rooms[i].GetComponent<EntitySpawner>().SpawnGunModification();
		}
    }
}
