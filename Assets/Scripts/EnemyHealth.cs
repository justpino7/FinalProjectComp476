using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float fleeThreshold = 50;
    public float kamikazeThreshold = 30;
    //public float steeringSpeed = 100f;
    public float fleeSpeed = 10f;
    public float kamikazeSpeed = 20f;
    private int currentHealth;
    public float kamikazeRotationSpeed = 360f; // Rotation speed in degrees per second


    private float hitCooldown = 0.5f;
    private float lastHitTime;

    private EnemyBehaviorTree enemyBehaviorTree;
    private EnemyController enemyController;

    private Transform playerTransform;
    private bool isFleeMode = false;
    private bool isKamikazeMode = false;

    public Vector3 fleePoint;
    public float maxPredictionTime = 1.0f;

    public AudioSource explosionAudio;
    public AudioSource hitAudio;

    void Start()
    {
        currentHealth = maxHealth;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyBehaviorTree = GetComponent<EnemyBehaviorTree>();
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        // Steering behaviors for Flee and Pursue
        if (isFleeMode)
        {
            Vector3 directionToFleePoint = (fleePoint - transform.position).normalized;
            transform.position += directionToFleePoint * fleeSpeed * Time.deltaTime;
        }
        else if (isKamikazeMode)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Calculate the prediction time based on the distance to the player
            float predictionTime = Mathf.Min(distance / kamikazeSpeed, maxPredictionTime);

            // Calculate the future position of the player
            Vector3 futurePosition = playerTransform.position + playerTransform.GetComponent<Rigidbody>().velocity * predictionTime;

            // Calculate the direction to the future position
            Vector3 directionToFuturePosition = (futurePosition - transform.position).normalized;

            transform.position += directionToFuturePosition * kamikazeSpeed * Time.deltaTime;
            // Rotate the enemy around the Z-axis
            transform.Rotate(0, 0, kamikazeRotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            if (Time.time - lastHitTime > hitCooldown)
            {
                lastHitTime = Time.time;
                hitAudio.Play();
                TakeDamage(10);
                Destroy(other.gameObject); // Destroy the bullet upon collision
            }
        }
        else if (isKamikazeMode && other.gameObject.CompareTag("Player"))
        {
            TakeDamage(currentHealth, false); // Make the enemy lose all its health and be destroyed
        }
    }

    void TakeDamage(int damage, bool givePoints = true)
    {
        currentHealth -= damage;

        if (currentHealth <= fleeThreshold && !isFleeMode && !isKamikazeMode)
        {
            isFleeMode = true;
            Debug.Log("Enemy starts fleeing!");
            enemyController.enabled = false;
            enemyBehaviorTree.enabled = false;
        }

        if (currentHealth <= kamikazeThreshold && !isKamikazeMode)
        {
            isKamikazeMode = true;
            Debug.Log("Enemy starts fleeing!");
            isFleeMode = false;
            enemyController.enabled = false;
            enemyBehaviorTree.enabled = false;
        }

        if (currentHealth <= 0)
        {
            explosionAudio.Play();
            if (givePoints)
            {
                ScoreManager.AddPoints(50);
            }
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}
