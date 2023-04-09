using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField]GameObject enemyGameobject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        float spacing = 2.0f; // spacing between enemies
        Vector3 centerPosition = transform.position; // center position of the game object

        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = centerPosition + new Vector3((i - 1) * spacing, 0, 0);
            Instantiate(enemyGameobject, spawnPosition, Quaternion.identity, transform);
        }
    }

}
