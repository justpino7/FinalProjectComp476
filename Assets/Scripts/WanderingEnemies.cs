using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEnemies : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 20f;
    private int currentWaypointIndex = 0;
    public AudioSource explosionAudio;

    private float hitCooldown = 0.5f;
    private float lastHitTime;

    void Start()
    {
        transform.position = waypoints[0].position;
    }

    void Update()
    {
        // Check if the AI enemy has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Increment the waypoint index to move to the next waypoint
            currentWaypointIndex++;

            // Check if the AI enemy has reached the last waypoint
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Reset the waypoint index to start moving from the beginning
                currentWaypointIndex = 0;
            }
        }

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            if (Time.time - lastHitTime > hitCooldown)
            {
                explosionAudio.Play();
                ScoreManager.AddPoints(10);
                lastHitTime = Time.time;
                Debug.Log("+ 10 points!");
                StartCoroutine(EnemyDeath());
            }
        }
    }

    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}
