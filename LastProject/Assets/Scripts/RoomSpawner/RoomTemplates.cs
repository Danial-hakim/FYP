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
	private bool spawnedBoss;
	public GameObject boss;

	public NavMeshSurface[] navMeshSurface;
	void Update()
	{
		if (waitTime <= 0 && spawnedBoss == false)
		{
			for (int i = 0; i < rooms.Count; i++)
			{
				if (i == rooms.Count - 1)
				{
					buildMeshNow();
					spawnedBoss = true;
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
        navMeshSurface = new NavMeshSurface[rooms.Count];
        for (int i = 0; i < rooms.Count; i++)
		{
			////Debug.Log(rooms[i].name);
			////navMeshSurface[i] = rooms[i].GetComponent<NavMeshSurface>();
            rooms[i].GetComponent<NavMeshSurface>().BuildNavMesh();
		}
		Debug.Log("BUILD DONE");
    }
}
