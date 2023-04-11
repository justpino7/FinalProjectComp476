using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 50f;
    public Transform[] startPoints;
    public GameObject[] destinationPoints;
    private Vector3 target;
    private int currentTargetIndex = 0;

    void Start()
    {
        // Choose a random starting point
        int randomIndex = Random.Range(0, startPoints.Length);
        transform.position = startPoints[randomIndex].position;

        // Choose a random destination point
        randomIndex = Random.Range(0, destinationPoints.Length);
        target = destinationPoints[randomIndex].transform.position;
    }

    void Update()
    {
        // Move towards the current destination point
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Rotate the asteroid to give a more realistic look
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Check if the asteroid has reached the current destination point
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Increment the destination point index to move to the next destination point
            currentTargetIndex++;

            // Check if the asteroid has reached the last destination point
            if (currentTargetIndex >= destinationPoints.Length)
            {
                // Destroy the asteroid when it reaches the last destination point
                Destroy(gameObject);
            }
            else
            {
                // Choose the next destination point
                target = destinationPoints[currentTargetIndex].transform.position;
            }
        }
    }
}
