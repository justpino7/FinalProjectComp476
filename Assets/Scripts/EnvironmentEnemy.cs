using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentEnemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;
    public float waitTime = 2.0f;

    private int currentWaypointIndex = 0;
    private float waypointReachedTime;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float shootingInterval = 3.0f;
    public float projectileSpeed = 40.0f;

    private GameObject player;
    private float lastShootTime;
    public float shootingRange = 50.0f;
    public int numberOfProjectiles = 8;

    public float aimRadius = 40.0f;
    public AudioSource explosionAudio;

    private float hitCooldown = 0.5f;
    private float lastHitTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastShootTime = Time.time;
    }

    private void Update()
    {
        Patrol();
        ShootAtPlayer();
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            if (Time.time - waypointReachedTime > waitTime)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                waypointReachedTime = Time.time;
            }
        }
        else
        {
            Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void ShootAtPlayer()
    {
        if (Time.time - lastShootTime > shootingInterval)
        {
            // Get the enemy's velocity
            Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
            Vector3 enemyVelocity = Vector3.zero;
            if (enemyRigidbody != null)
            {
                enemyVelocity = enemyRigidbody.velocity;
            }

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation * Quaternion.Euler(0, 90, 0));
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

            // Add constraints to the projectile's rigidbody
            projectileRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            Vector3 direction;

            // Check if the player is within the specified radius
            if (Vector3.Distance(transform.position, player.transform.position) <= aimRadius)
            {
                // Aim at the player's predicted position
                Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
                Vector3 playerVelocity = playerRigidbody != null ? playerRigidbody.velocity : Vector3.zero;

                float timeToReachPlayer = Vector3.Distance(transform.position, player.transform.position) / projectileSpeed;
                Vector3 predictedPosition = player.transform.position + playerVelocity * timeToReachPlayer;

                direction = (predictedPosition - transform.position).normalized;
            }
            else
            {
                // Shoot forward
                direction = transform.forward;
            }

            projectileRigidbody.AddForce((direction * projectileSpeed) + enemyVelocity, ForceMode.VelocityChange);

            // Destroy the projectile after 2 seconds
            Destroy(projectile, 2f);

            lastShootTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            if (Time.time - lastHitTime > hitCooldown)
            {
                explosionAudio.Play();
                ScoreManager.AddPoints(15);
                lastHitTime = Time.time;
                Debug.Log("+ 15 points!");
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