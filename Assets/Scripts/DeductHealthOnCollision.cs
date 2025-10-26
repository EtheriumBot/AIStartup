using UnityEngine;

public class DeductHealthOnCollision : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damageAmount = 1;

    // Store the component reference once
    private PlayerHealth playerHealth;
    private float dist;

    void Start()
    {
        // Find the player object by tag and get the PlayerHealth component from it
        GameObject playerObject = GameObject.Find("Hitbox");
        if (playerObject != null)
        {
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }

        if (playerHealth == null)
        {
            Debug.LogError("Player object is found, but is missing the PlayerHealth script! Check the Player GameObject.");
        }
    }

    void Update()
    {
        // Check if we have a valid component reference
        if (playerHealth != null)
        {
            dist = Vector3.Distance(this.transform.position, new Vector3(0, 0, 0));

            if (dist < 0.5f)
            {
                Debug.Log("I'm deducting your health!");
                Debug.Log("Before health:" + playerHealth.health);

                // Deduct health from the player
                playerHealth.health -= damageAmount;

                Debug.Log("health:" + playerHealth.health);
                Destroy(gameObject); // Destroy the enemy after it hits
            }
        }
        else
        {
            // Only log an error if you're expecting to find the Player and haven't yet
            // This is the fallback for when 'GameObject.FindWithTag' fails
        }
    }
}