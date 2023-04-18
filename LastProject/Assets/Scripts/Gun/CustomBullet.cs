using System.Collections.Generic;
using UnityEngine;

/// Thanks for downloading my custom bullets/projectiles script! :D
/// Feel free to use it in any project you like!
/// 
/// The code is fully commented but if you still have any questions
/// don't hesitate to write a yt comment
/// or use the #coding-problems channel of my discord server
/// 
/// Dave
public class CustomBullet : MonoBehaviour
{
    public enum ElementType
    {
        Fire,
        Water,
        Ice,
        Lightning,
        None
    }
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public GameObject lightningExplosion;
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    float explosionRange;
    public float explosionForce;
    float damage;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    //Element Color
    Color currentColor;
    ElementType currentElement;

    bool triggerStatusAilment = true;
    float statusAilmentDuration = 2.0f;

    private void Start()
    {
        Setup();
        //currentElement = ElementType.None;
    }

    private void Update()
    {
        //When to explode:
        if (collisions > maxCollisions) Explode();

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode()
    {
        //Instantiate explosion
        if(currentElement == ElementType.Lightning)
        {
            Instantiate(lightningExplosion, transform.position, Quaternion.identity);
            explosionRange = 3;
        }
        else
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            explosionRange = 1;
        }
        //if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //Check for enemies 
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            //Get component of enemy and call Take Damage
            CheckElement(enemies[i].GetComponent<Enemy>());
            enemies[i].GetComponent<Enemy>().TakeDamageEffect(0.2f,damage);

            //Add explosion force (if enemy has a rigidbody)
            if (enemies[i].GetComponent<Rigidbody>())
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
        }

        Delay();
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Count up collisions
        collisions++;

        //Explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("Enemy") && explodeOnTouch)
        {
            Explode();
        }         
    }

    private void Setup()
    {
        //Create a new Physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;

        //Stats 
        damage = 10;
    }

    /// Just to visualize the explosion range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    private Dictionary<ElementType, System.Action<Enemy>> elementEffects = new Dictionary<ElementType, System.Action<Enemy>>()
    {
        { ElementType.Fire, (enemy) => enemy.onFire = true },
        { ElementType.Water, (enemy) => enemy.beingSlowed = true },
        { ElementType.Ice, (enemy) => enemy.currentlyFrozen = true },
        { ElementType.Lightning, (enemy) => enemy.increaseLihgtningResistance() },
    };

    private void CheckElement(Enemy enemy)
    {
        System.Action<Enemy> effect;
        if (elementEffects.TryGetValue(currentElement, out effect))
        {
            effect(enemy);
        }
    }

    public void updateBulletElement()
    {
        currentColor = gameObject.GetComponent<Renderer>().material.color;

        // Define a dictionary that maps colors to element types
        Dictionary<Color, ElementType> colorToElementMap = new Dictionary<Color, ElementType>
        {
            { Color.red, ElementType.Fire },
            { Color.blue, ElementType.Water },
            { Color.cyan, ElementType.Ice },
            { Color.yellow, ElementType.Lightning }
        };

        // Check if the current color is in the dictionary, and assign the corresponding element type
        if (colorToElementMap.TryGetValue(currentColor, out ElementType elementType))
        {
            currentElement = elementType;
        }
        else
        {
            currentElement = ElementType.None;
        }
    }
}
