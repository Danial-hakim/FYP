using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField]GameObject enemy;
    [SerializeField]GameObject player;

    [SerializeField]GameObject[] bulletElemets;
    [SerializeField]GameObject[] gunModification;
    [SerializeField]GameObject healZone;
    [SerializeField]GameObject[] wall_Layouts;

    int counter;
    private int previousCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(previousCount > 0) 
        {
            counter = CalculateRemainingEnemies();
            if (counter != previousCount)
            {
                Debug.Log(gameObject.name + "Has " + counter + " Enemies left");
                previousCount = counter;
            }

            if (counter == 0)
            {
                Debug.Log("done");
            }
        }   
    }

    public void SpawnEnemies()
    {
        float spacing = 4.0f; // spacing between enemies
        Vector3 centerPosition = transform.position; // center position of the game object

        for (int i = 0; i < 2; i++)
        {
            Vector3 spawnPosition = centerPosition + new Vector3((i - 1) * spacing, 0, 0);
            Instantiate(enemy, spawnPosition, Quaternion.identity, transform);
        }

        previousCount = CalculateRemainingEnemies();
    }

    public void SpawnPlayer()
    {
        Vector3 centerPosition = transform.position;

        Instantiate(player, centerPosition, Quaternion.identity);
    }

    public void SpawnBulletElements()
    {
        float spacing = 6.0f;
        int rand = Random.Range(0, gunModification.Length);
        Vector3 centerPosition = transform.position + new Vector3(spacing, -1.5f, 0);
        Instantiate(bulletElemets[rand], centerPosition, Quaternion.identity, transform);
    }

    public void SpawnGunModification()
    {
        float spacing = -6.0f;
        Vector3 centerPosition = transform.position + new Vector3(spacing, -1f, 0);
        int rand = Random.Range(0, gunModification.Length);
        Instantiate(gunModification[rand], centerPosition, Quaternion.identity, transform);
    }

    public void SpawnHealZone()
    {
        float cornerCorrection = 7f;
        Vector3 centerPosition = transform.position + new Vector3(cornerCorrection, -1f, cornerCorrection);
        Instantiate(healZone, centerPosition, Quaternion.identity, transform);
    }

    int CalculateRemainingEnemies()
    {
        int count = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Enemy")
            {
                // Increment count for each child with matching name
                count++;
            }
        }
        return count;
    }

    public void SpawnWallLayouts()
    {
        Vector3 centerPosition = transform.position;
        int rand = Random.Range(0, wall_Layouts.Length);
        Instantiate(wall_Layouts[rand], centerPosition, Quaternion.identity, transform);
    }
}
