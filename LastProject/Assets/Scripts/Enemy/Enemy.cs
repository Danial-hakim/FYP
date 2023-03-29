using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent; }

    private Color originalColor;
    private new Renderer renderer;
    private bool isImmuneToDamage;

    [SerializeField]
    private string currentState;
    public Path path;

    float health = 100;
    public Slider healthBar;

    public GameObject explosion;

    //statusAilment
    public bool onFire { get; set; } = false;
    float fireDuration = 5;

    public bool beingSlowed { get; set; } = false;
    float slowedDuration = 3;

    public bool currentlyFrozen { get; set; } = false;
    float freezeDuration = 2;
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
        healthBar.value = health;
    }

    public void TakeDamageEffect(float immuneDuration, float damage)
    {
        // Get the renderer component of the game object
        renderer = GetComponent<Renderer>();

        // Check if the renderer is not null
        if (renderer != null)
        {
            // Start a coroutine that flashes the game object red and back to its original color
            StartCoroutine(FlashColor(renderer, immuneDuration));
        }
        if(onFire)
        {
            StartCoroutine(TakeFireDamage());
        }
        else if(beingSlowed)
        {
            StartCoroutine(ReduceMoveSpeed());
        }
        else if(currentlyFrozen)
        {
            StartCoroutine(Frozen());
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

    IEnumerator TakeFireDamage()
    {
        for(int i = 0; i < fireDuration;i++)
        {           
            yield return new WaitForSeconds(1f);
            health -= 1;
        }

        onFire = false;
    }

    IEnumerator ReduceMoveSpeed()
    {
        float currentSpeed = agent.speed;
        agent.speed = currentSpeed * 0.5f;
        yield return new WaitForSeconds(slowedDuration);
        agent.speed = currentSpeed;
        beingSlowed = false;
    }

    IEnumerator Frozen()
    {
        float currentSpeed = agent.speed;
        agent.speed = 0;
        yield return new WaitForSeconds(freezeDuration);
        agent.speed = currentSpeed;
        beingSlowed = false;
    }
}
  