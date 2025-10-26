using UnityEngine;

public class BalloonBehavior : MonoBehaviour
{
    public Transform witch;              // Target (the Witch / player)
    public GameObject bombPrefab;        // Bomb prefab to drop
    public float moveSpeed = 2f;         // How fast the balloon moves toward the witch
    public float hoverHeight = 0.5f;     // Amplitude of the hover motion
    public float hoverSpeed = 2f;        // How fast it hovers up and down
    public float dropRange = 1.5f;       // How close above the witch before dropping
    public float dropCooldown = 3f;      // Seconds between bomb drops

    private Vector3 startPosition;
    private float lastDropTime;

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

        lastDropTime = -dropCooldown; // So it can drop immediately if in range
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

        // --- Drop bomb when above the witch ---
        TryDropBomb();
    }

    void TryDropBomb()
    {
        // Check if balloon is roughly above witch horizontally
        float horizontalDistance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(witch.position.x, 0, witch.position.z)
        );

        bool isAbove = horizontalDistance < dropRange;
        bool cooldownOver = Time.time - lastDropTime >= dropCooldown;

        if (isAbove && cooldownOver && bombPrefab != null)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
            lastDropTime = Time.time;
        }
    }
}
