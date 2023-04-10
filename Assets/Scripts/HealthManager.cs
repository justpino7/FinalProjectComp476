using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int health = 100;
    public AudioSource hitAudio;
    public AudioSource playerDeathAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Structure"))
        {
            Debug.Log("Collide with structure");
            hitAudio.Play();
            TakeDamage(10);
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collide with Enemy");
            hitAudio.Play();
            TakeDamage(20);
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
    }
}
