using NUnit.Framework.Constraints;
using UnityEngine;

public class BalloonBehavior : MonoBehaviour
{
    public Transform witch;        // Target (the Witch)
    public float moveSpeed = 2f;   // How fast the balloon moves toward the witch
    public float hoverHeight = 0.5f; // Amplitude of the hover motion
    public float hoverSpeed = 2f;  // How fast it hovers up and down

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        // Automatically find the witch if not assigned
        if (witch == null)
        {
            GameObject witchObject = GameObject.Find("Witch");
            if (witchObject != null)
                witch = witchObject.transform;
            else
                Debug.LogError("Witch not found in scene!");
        }
    }

    void Update()
    {
        if (witch == null) return;
        
        float yPos = transform.position.y;

        // --- Move toward the witch ---
        Vector3 targetPosition = new Vector3(witch.position.x, yPos, witch.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // --- Hovering motion ---
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position += Vector3.up * hoverOffset * Time.deltaTime;

        // --- (Optional) Slight rotation for realism ---
        transform.Rotate(0f, Mathf.Sin(Time.time * 1.5f) * 10f * Time.deltaTime, 0f);
    }
}
