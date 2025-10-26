using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public AudioClip shootSFX;        // assign in Inspector (arrow_shoot)
    public float pitchVariance = 0.05f; // optional small random pitch

    private AudioSource audioSource;

    void Start()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        // existing collision handling...
        Destroy(gameObject);
    }
}
