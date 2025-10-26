using UnityEngine;

public class BalloonDropper : MonoBehaviour
{
    public GameObject bomb;        // Assign your Bomb (child of balloon)
    public float dropHeight = 3f;  // Height at which bomb drops
    private bool hasDropped = false;

    void Update()
    {
        // When the balloon comes down below a certain height
        if (!hasDropped && transform.position.y <= dropHeight)
        {
            DropBomb();
        }
    }

    void DropBomb()
    {
        if (bomb == null) return;

        // Unparent bomb so it can fall freely
        bomb.transform.parent = null;

        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        hasDropped = true;
    }
}
