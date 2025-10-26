using UnityEngine;

public class Arrow : MonoBehaviour
{

    // Assign your blood splatter Particle System Prefab in the Inspector
    public GameObject bloodSplatterPrefab;
    public float speed = 30f; // Set a movement speed in the Inspector

    public AudioClip shootSFX;        // assign in Inspector (arrow_shoot)

    public float pitchVariance = 0.05f; // optional small random pitch
    private AudioSource audioSource;

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

        // Get or add an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // Make sure AudioSource is configured for 3D in the inspector if desired
        // Play the shooting sound once when arrow is spawned
        if (shootSFX != null)
        {
            audioSource.pitch = 1f + Random.Range(-pitchVariance, pitchVariance);
            audioSource.PlayOneShot(shootSFX);
        }

        // Optional: destroy after X seconds so sounds don't linger with pooled arrows
        Destroy(gameObject, 10f);

    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
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
