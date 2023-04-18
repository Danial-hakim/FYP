using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField]GameObject enemyGameobject;
    [SerializeField]GameObject playerGameobject;

    [SerializeField]GameObject bulletElemets;
    [SerializeField]GameObject[] gunModification;
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
        float spacing = 4.0f; // spacing between enemies
        Vector3 centerPosition = transform.position; // center position of the game object

        for (int i = 0; i < 2; i++)
        {
            Vector3 spawnPosition = centerPosition + new Vector3((i - 1) * spacing, 0, 0);
            Instantiate(enemyGameobject, spawnPosition, Quaternion.identity, transform);
        }
    }

    public void SpawnPlayer()
    {
        Vector3 centerPosition = transform.position;

        Instantiate(playerGameobject, centerPosition, Quaternion.identity);
    }


    public void SpawnBulletElements()
    {
        Debug.Log("SPAWN bullet");
        //float spacing = 8.0f;
        //Vector3 centerPosition = transform.position + new Vector3(spacing, 0, 0);
        //Instantiate(bulletElemets, centerPosition, Quaternion.identity, transform);
    }

    public void SpawnGunModification()
    {
        Vector3 centerPosition = transform.position;
        int rand = Random.Range(0, gunModification.Length);
        Instantiate(gunModification[rand], centerPosition, Quaternion.identity, transform);
    }
}
