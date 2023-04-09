using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform crosshair;
    public float velocity = 10.0f;
    float _destroyTime = 3.0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown("space"))
        {
            Rigidbody newBullet = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;

            // Get the Arwing's velocity
            Rigidbody arwingRigidbody = GetComponentInParent<Rigidbody>();
            Vector3 arwingVelocity = Vector3.zero;
            if (arwingRigidbody != null)
            {
                arwingVelocity = arwingRigidbody.velocity;
            }

            // Calculate the direction to shoot the bullet towards the crosshair
            Vector3 shootDirection = (crosshair.position - transform.position).normalized;

            // Add the Arwing's velocity to the bullet's velocity and aim towards the crosshair
            newBullet.AddForce((shootDirection * velocity) + arwingVelocity, ForceMode.VelocityChange);
            Destroy(newBullet.gameObject, _destroyTime);
        }
    }
}
