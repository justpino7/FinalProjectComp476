using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Cinemachine;

public class HealthManager : MonoBehaviour
{
    public int health = 100;
    public AudioSource hitStructureAudio;
    public AudioSource hitEnemyAudio;
    public AudioSource playerDeathAudio;
    public CinemachineDollyCart dollyCart;
    public AudioSource barrelRollBlockAudio;

    public GameObject endMenu;

    public GameObject gameUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Structure"))
        {
            Debug.Log("Collided with structure");
            hitStructureAudio.Play();
            TakeDamage(10);

            // Start the tilting effect
            StartTilting(2.0f, 45.0f); // Adjust the duration and angle as needed
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Enemy");
            hitEnemyAudio.Play();
            TakeDamage(20);
        }

        else if (other.CompareTag("EnemyAI"))
        {
            Debug.Log("Kamikaze enemy crashed");
            hitEnemyAudio.Play();
            TakeDamage(20);
        }

        else if (other.CompareTag("EnemyBullet"))
        {
            // Only take damage if the player is not "Invincible" 
            if (gameObject.CompareTag("Player"))
            {
                Debug.Log("Collided with Enemy Bullet!");
                hitEnemyAudio.Play();
                TakeDamage(15);
            }

            if (gameObject.CompareTag("Invincible"))
            {
                barrelRollBlockAudio.Play();
            }

        }

        else if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Collided with Asteroid... Ouch!");
            hitStructureAudio.Play();
            TakeDamage(25);
        }
        else if (other.CompareTag("End"))
        {
            Debug.Log("End of the game!");
            // Freeze the scene and activate the end menu
            Time.timeScale = 0f;
            endMenu.SetActive(true);
            gameUI.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            playerDeathAudio.Play();
          

            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);

        // Freeze the scene and activate the end menu
        Time.timeScale = 0f;
        endMenu.SetActive(true);
        gameUI.SetActive(false);
    }

    public void StartTilting(float duration, float angle)
    {
        StartCoroutine(Tilting(duration, angle));
    }

    private IEnumerator Tilting(float duration, float angle)
    {
        float elapsedTime = 0f;
        float halfDuration = duration / 2f;
        bool tiltRight = true;

        Quaternion initialRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);

        while (elapsedTime < duration)
        {
            float t = (elapsedTime % halfDuration) / halfDuration;

            if (tiltRight)
            {
                transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            }
            else
            {
                transform.localRotation = Quaternion.Lerp(targetRotation, initialRotation, t);
            }

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= halfDuration)
            {
                tiltRight = !tiltRight;
                halfDuration = duration - halfDuration; // Swap the half duration after the first tilt
            }

            yield return null;
        }

        // Reset rotation to the initial rotation
        transform.localRotation = initialRotation;
    }

}
