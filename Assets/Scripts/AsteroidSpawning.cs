using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawning : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform[] startPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, startPoints.Length);
                Instantiate(asteroidPrefab, startPoints[randomIndex].position, Quaternion.identity);
            }
        }
    }
}
