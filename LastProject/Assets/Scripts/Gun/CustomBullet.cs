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
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;
    public float damage;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    //Element Color
    Color currentColor;
    ElementType currentElement;

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
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //Check for enemies 
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            //Get component of enemy and call Take Damage

            enemies[i].GetComponent<Enemy>().TakeDamageEffect(0.2f,damage);
            CheckElement(enemies[i].GetComponent<Enemy>());

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
    }

    /// Just to visualize the explosion range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    private void CheckElement(Enemy enemy) 
    {
       switch(currentElement) 
       {
            case ElementType.Fire:
                enemy.setOnFire();
                //Debug.Log("fire function");
                break;
            case ElementType.Water:
                Debug.Log("water function");
                break;
            case ElementType.Ice:
                Debug.Log("ice function");
                break;
            case ElementType.Lightning:
                Debug.Log("lightning function");
                break;
            default:
                Debug.Log("None");
                break;
       };
    }

    public void updateBulletElement()
    {
        currentColor = gameObject.GetComponent<Renderer>().material.color;

        if (currentColor == Color.red) { currentElement = ElementType.Fire; }
        else if (currentColor == Color.blue) { currentElement = ElementType.Water; }
        else if (currentColor == Color.cyan) { currentElement = ElementType.Ice; }
        else if (currentColor == Color.yellow) { currentElement = ElementType.Lightning; }
        else { currentElement = ElementType.None; };
    }
}
