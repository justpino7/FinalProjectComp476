using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int asteroidCount = 3;
    public float spawnRate = 5.0f;
    public Vector3 spawnAreaMin, spawnAreaMax;
    public Vector3 targetAreaMin, targetAreaMax;
    public float asteroidSpeed = 10.0f;

    private float lastSpawnTime;
    private bool startSpawning = false;

    public void StartSpawning()
    {
        startSpawning = true;
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (startSpawning && Time.time - lastSpawnTime > spawnRate)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                SpawnAsteroid();
            }
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnAsteroid()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        Vector3 targetPosition = new Vector3(
            Random.Range(targetAreaMin.x, targetAreaMax.x),
            Random.Range(targetAreaMin.y, targetAreaMax.y),
            Random.Range(targetAreaMin.z, targetAreaMax.z)
        );

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Rigidbody asteroidRigidbody = asteroid.GetComponent<Rigidbody>();

        Vector3 direction = (targetPosition - spawnPosition).normalized;
        asteroidRigidbody.AddForce(direction * asteroidSpeed, ForceMode.VelocityChange);

        asteroidRigidbody.AddTorque(Random.insideUnitSphere * 10, ForceMode.VelocityChange);

        // Destroy the asteroid after 5 seconds
        Destroy(asteroid, 8f);
    }
}
