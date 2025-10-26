using UnityEngine;

public class Arrow : MonoBehaviour
{

    // Assign your blood splatter Particle System Prefab in the Inspector
    public GameObject bloodSplatterPrefab;
    public float speed = 30f; // Set a movement speed in the Inspector

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Ensure the arrow flies forward using physics velocity
        if (rb != null)
        {
            // Move forward based on the arrow's initial rotation (transform.forward)
            rb.linearVelocity = transform.forward * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    // Use OnCollisionEnter for physics-based impacts
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Arrow collided with: " + collision.gameObject.name);

        // Check if the collided object has the tag "Skeleton"
        // It's best practice to tag your enemy GameObjects
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Debug.Log("Arrow hit an enemy!");

            // 1. Get the EnemyHealth script from the enemy
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // 2. Deduct the enemy's health by 1
                enemyHealth.TakeDamage(1);

            }

            // 3. Spawn a particle system blood splatter
            // Use the contact point for accurate placement
            ContactPoint contact = collision.contacts[0];
            Instantiate(
                bloodSplatterPrefab,
                contact.point,
                Quaternion.LookRotation(contact.normal) // Orient the splatter to face away from the surface
            );

            // 4. Destroy the arrow GameObject
            Destroy(gameObject);
        }
    }
}
