using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    private AudioSource arrowAudio;
    public AudioClip shootClip;

    void Start()
    {
        if (arrowAudio == null)
            arrowAudio = GetComponent<AudioSource>();
    }

    public void ShootArrow()
    {
        // Your arrow shooting logic here...

        // Play sound
        arrowAudio.PlayOneShot(shootClip);
    }
}