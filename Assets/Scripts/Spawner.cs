using UnityEngine;

public class Spawner : MonoBehaviour
{
    // New public variables for min/max radius
    [Header("Spawn Radius Constraints")]
    public float minSpawnRadius = 5f; // New minimum distance from spawner
    public float maxSpawnRadius = 15f; // Updated maximum distance

    [Header("Prefabs to Spawn")]
    public GameObject skeletonPrefab;
    public GameObject balloonPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;   // Time between spawns
    // Removed the old 'spawnRadius' since we now have min/max

    public float balloonSpawnChance = 0.3f; // 40% chance balloon, 60% skeleton

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandom();
            timer = 0f;
        }
    }

    void SpawnRandom()
    {
        // 1. Calculate a random distance between the min and max radius
        float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);

        // 2. Generate a random point on a unit circle (radius of 1)
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * randomDistance;

        // 3. Convert the 2D point to a 3D position relative to the spawner
        // We use the spawner's transform.position as the center
        Vector3 randomPos = transform.position + new Vector3(
            randomCirclePoint.x,
            0f, // Keeping the spawn at the spawner's y-level (ground)
            randomCirclePoint.y
        );

        // --- The rest of your existing logic remains the same ---

        // Randomly decide what to spawn
        GameObject prefabToSpawn = (Random.value < balloonSpawnChance) ? balloonPrefab : skeletonPrefab;

        // Spawn the object
        if (prefabToSpawn == balloonPrefab)
        {
            // Note: If your balloon is flying, you might want to consider the terrain height here.
            randomPos = new Vector3(randomPos.x, randomPos.y + 2, randomPos.z);
            Instantiate(prefabToSpawn, randomPos, Quaternion.identity);
        }
        else
        {
            Instantiate(prefabToSpawn, randomPos, Quaternion.identity);
        }
    }
}