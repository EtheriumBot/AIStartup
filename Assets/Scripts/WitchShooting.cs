using UnityEngine;

public class WitchShooting : MonoBehaviour
{
    [Header("Projectile & Target")]
    public GameObject fireballPrefab; // Assign your fireball prefab in the Inspector
    public Transform target;          // Reference to the player's position (or main camera)
    public Transform firePoint;       // The point/location where the fireball will spawn

    [Header("Shooting Settings")]
    public float fireInterval = 2f;   // Time between each shot
    public float projectileSpeed = 2f; // How fast the fireball moves

    private float fireTimer;

    void Start()
    {
        // Initialize the timer so the first shot fires after the full interval
        fireTimer = fireInterval;

        // Find the player automatically if the target hasn't been set in the Inspector
        if (target == null)
        {
            // Assuming the player/camera has the tag "Player" or is the OVRCameraRig
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void Update()
    {
        // Only shoot if we have a target
        if (target == null) return;

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0)
        {
            ShootFireball();
            fireTimer = fireInterval; // Reset the timer
        }
    }

    void ShootFireball()
    {
        // 1. Calculate the direction from the enemy's firing point to the target
        Vector3 directionToTarget = (target.position - firePoint.position).normalized;

        // 2. Adjust enemy rotation to face the target (optional, but makes it look better)
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

        // 3. Instantiate the fireball at the fire point's position and rotation
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, lookRotation);

        // 4. Get the fireball's Rigidbody and set its velocity
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply a force or set the velocity in the calculated direction
            rb.linearVelocity = directionToTarget * projectileSpeed;
        }
        else
        {
            Debug.LogError("Fireball Prefab is missing a Rigidbody component!");
        }
    }
}