using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;       // Movement speed
    public float lifetime = 5f;     // How long before it destroys itself
    public GameObject explosionEffect; // Optional: particle effect prefab on hit or expire

    void Start()
    {
        // Automatically destroy fireball after its lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move forward continuously
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // (Optional) Instantiate explosion effect on impact
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Destroy the fireball on contact
        Destroy(gameObject);
    }
}
