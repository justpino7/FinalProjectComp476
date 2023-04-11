using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidTrigger : MonoBehaviour
{
    public AsteroidSpawner asteroidSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Asteroids shower incoming");
            asteroidSpawner.StartSpawning();
        }
    }
}
