using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs to Spawn")]
    public GameObject skeletonPrefab;
    public GameObject balloonPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;   // Time between spawns
    public float spawnRadius = 10f;    // How far from the spawner to spawn objects
    public float balloonSpawnChance = 0.4f; // 40% chance balloon, 60% skeleton

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
        // Pick a random position around the spawner
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0f,
            Random.Range(-spawnRadius, spawnRadius)
        );

        // Randomly decide what to spawn
        GameObject prefabToSpawn = (Random.value < balloonSpawnChance) ? balloonPrefab : skeletonPrefab;

        // Spawn the object
        if (prefabToSpawn == balloonPrefab)
        {
            randomPos = new Vector3(randomPos.x, randomPos.y + 2, randomPos.z);
            Instantiate(prefabToSpawn, randomPos, Quaternion.identity);
        } else
        {
            Instantiate(prefabToSpawn, randomPos, Quaternion.identity);
        }
    }
}
