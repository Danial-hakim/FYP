using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
    static float fireDuration = 5;
    float startingFireDuration = 5;

    public bool beingSlowed { get; set; } = false;
    static float slowedDuration = 4;
    float startingSlowedDuration = 4;

    public bool currentlyFrozen { get; set; } = false;
    static float freezeDuration = 2;
    float startingFreezeDuration = 2;

    static float damageReduction = 0;
    float startingDamageReduction = 0;
    //Adaptive
    static float fireResistance = 0.0f; // reduce the damage overtime
    static float waterResistance = 0.0f; // reduce the slow reduction
    static float iceResistance = 0.0f; // reduce the freeze duration 
    static float lightningResistance = 0.0f; // reduce the damage 
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
        UpdateResistance();
        UpdateStatusAilment();
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
            health -= 5f;
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

    public void increaseLihgtningResistance()
    {
        if(lightningResistance < 70)
        {
            lightningResistance += 1f;
        }
        fireResistance -= 2f;
        waterResistance -= 2f;
        iceResistance -= 2f;
    }

    static Dictionary<string, float> resistances = new Dictionary<string, float>()
    {
        {"Fire", fireResistance},
        {"Water", waterResistance},
        {"Ice", iceResistance},
        {"Lightning", lightningResistance},
    };

    List<(string element, float multiplier)> resistanceModifiers = new List<(string, float)>
    {
        ("Fire", 2f),
        ("Water", 2f),
        ("Ice", 2f),
        ("Lightning", 1f)
    };

    void UpdateResistance()
    {
        string currentElement = "None";

        foreach (var modifier in resistanceModifiers)
        {
            if ((onFire && modifier.element == "Fire") ||
                (beingSlowed && modifier.element == "Water") ||
                (currentlyFrozen && modifier.element == "Ice"))
            {
                resistances[modifier.element] += modifier.multiplier;
                currentElement = modifier.element;
            }
        }

        foreach (var otherModifier in resistanceModifiers)
        {
            if (otherModifier.element != currentElement)
            {
                if (resistances[otherModifier.element] > 0)
                {
                    resistances[otherModifier.element] -= otherModifier.multiplier;
                }
            }
        }

        fireResistance = resistances["Fire"];
        waterResistance = resistances["Water"];
        iceResistance = resistances["Ice"];
        lightningResistance = resistances["Lightning"];
    }

    void UpdateStatusAilment()
    {
        const float MAX_PERCENTAGE = 100f;
        const float PERCENTAGE_MULTIPLIER = 0.01f;

        float remainingPercentage = MAX_PERCENTAGE - fireResistance * PERCENTAGE_MULTIPLIER;

        // Calculate the duration of the fire ailment based on the remaining percentage
        fireDuration = RemainderCalculation(startingFireDuration,fireResistance);

        // Calculate the duration of the slowed ailment based on the remaining percentage
        slowedDuration = RemainderCalculation(startingSlowedDuration,waterResistance);

        // Calculate the duration of the freeze ailment based on the remaining percentage
        freezeDuration = RemainderCalculation(startingFreezeDuration,iceResistance);

        // TODO: Calculate the damage reduction based on the remaining percentage
        // damageReduction = damageReduction * remainingPercentage;
        Debug.Log(slowedDuration);
    }

    float RemainderCalculation(float startingDuration, float resistanceType)
    {
        const float MAX_PERCENTAGE = 100f;
        const float PERCENTAGE_MULTIPLIER = 0.01f;

        return startingDuration * ((MAX_PERCENTAGE - resistanceType) * PERCENTAGE_MULTIPLIER);
    }
}
  