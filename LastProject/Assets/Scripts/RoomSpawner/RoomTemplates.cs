using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

	int clearedRoomCounter;
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

		foreach(var room in rooms)
		{
			if(room.GetComponent<EntitySpawner>().cleared)
			{
				clearedRoomCounter++;
			}
			else
			{
				clearedRoomCounter = 0;
				break;
			}
		}

		if(clearedRoomCounter == rooms.Count)
		{
			Debug.Log("Spawn boss");
		}
	}

	void buildMeshNow()
	{
        foreach (var room in rooms)
        {
            // Skip the first room (index 0)
            if (room == rooms[0])
			{
				room.GetComponent<EntitySpawner>().SpawnPlayer();
				room.GetComponent<EntitySpawner>().cleared = true;
                continue;
            }

            // Spawn wall layouts for each room
            room.GetComponent<EntitySpawner>().SpawnWallLayouts();

            // Build navmesh for each room
            room.GetComponent<NavMeshSurface>().BuildNavMesh();

            // Spawn heal zones, enemies, bullet elements, and gun modifications for each room
            room.GetComponent<EntitySpawner>().SpawnHealZone();
            room.GetComponent<EntitySpawner>().SpawnEnemies();
            room.GetComponent<EntitySpawner>().SpawnBulletElements();
            room.GetComponent<EntitySpawner>().SpawnGunModification();
        }
    }
}
