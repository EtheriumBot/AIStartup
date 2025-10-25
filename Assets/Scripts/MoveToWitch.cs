using UnityEngine;

public class MoveToWitch : MonoBehaviour
{
    private Transform witch;      // Reference to the Witch's transform
    public float speed = 3f;     // Movement speed

    void Start()
    {
        // If you forgot to assign the Witch in the Inspector, try finding it by name
        if (witch == null)
        {
            GameObject witchObject = GameObject.Find("Witch");
            if (witchObject != null)
            {
                witch = witchObject.transform;
            }
            else
            {
                Debug.LogError("Witch not found in scene!");
            }
        }
    }

    void Update()
    {
        if (witch != null)
        {
            // Move toward the Witch
            transform.position = Vector3.MoveTowards(
                transform.position,
                witch.position,
                speed * Time.deltaTime
            );

            // (Optional) Make the skeleton face the witch
            transform.LookAt(witch);
        }
    }
}
