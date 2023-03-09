using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent; }

    private Color originalColor;
    private Renderer renderer;
    public bool isImmuneToDamage;

    [SerializeField]
    private string currentState;
    public Path path;

    float health = 5;

    public GameObject explosion;

    public GameObject player;

    int chaseRange = 5;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();

        renderer = GetComponent<Renderer>();
        isImmuneToDamage = false;
        // Check if the renderer is not null
        if (renderer != null)
        {
            // Save the original color of the game object
            originalColor = renderer.material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange();
    }

    public void TakeDamageEffect(float immuneDuration, float damage)
    {
        Debug.Log("Hit");
        // Get the renderer component of the game object
        renderer = GetComponent<Renderer>();

        // Check if the renderer is not null
        if (renderer != null)
        {
            // Start a coroutine that flashes the game object red and back to its original color
            StartCoroutine(FlashColor(renderer, immuneDuration));
        }
        calculateRemainingHealth(damage);
    }

    IEnumerator FlashColor(Renderer renderer, float immuneDuration)
    {
        isImmuneToDamage = true;
        // Set the game object's color to red
        renderer.material.color = Color.red;
        // Wait for 0.1 seconds
        yield return new WaitForSeconds(immuneDuration);
        isImmuneToDamage = false;
        // Set the game object's color back to its original color
        renderer.material.color = originalColor;
    }

    void Explode()
    {
        Instantiate(explosion,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void calculateRemainingHealth(float damageTaken)
    {
        health -= damageTaken;

        if(health < 0)
        {
            Explode();
        }
    }

    void playerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);

        if (Vector3.Distance(transform.position,player.transform.position) < chaseRange)
        {
            
        }
    }
}
