using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class EnemySpawnerTrigger : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public float spawnDistanceFromCamera;
    public Transform[] spawnPoints;
    public Transform[] destinationPoints;


    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy wave incoming!");
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        // Shuffle the destination points
        System.Random rnd = new System.Random();
        destinationPoints = destinationPoints.OrderBy(x => rnd.Next()).ToArray();

        // Shuffle the spawn points
        spawnPoints = spawnPoints.OrderBy(x => rnd.Next()).ToArray();

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Use the spawn points as the spawn positions
            Vector3 spawnPosition = spawnPoints[i % spawnPoints.Length].position;
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            RailEnemy railEnemy = enemy.GetComponent<RailEnemy>();

            // Assign the shuffled destination points to the enemies
            Transform destination = destinationPoints[i % destinationPoints.Length];
            railEnemy.SetDestination(destination);
        }
    }
}
