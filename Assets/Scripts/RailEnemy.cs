using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailEnemy : MonoBehaviour
{
    public float arriveDistance = 0.5f;
    public float speed = 50.0f;

    private Transform destination;
    private Cinemachine.CinemachineDollyCart dollyCart;

    private void Start()
    {
        // Find the Dolly Cart object in the scene using its tag
        GameObject dollyCartObject = GameObject.FindGameObjectWithTag("DollyCart");
        dollyCart = dollyCartObject.GetComponent<Cinemachine.CinemachineDollyCart>();
    }

    private void Update()
    {
        if (destination != null)
        {
            float distance = Vector3.Distance(transform.position, destination.position);

            if (distance > arriveDistance)
            {
                // Move towards the destination
                Vector3 direction = (destination.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
            else
            {
                // Set the invisible plane as the parent
                transform.SetParent(destination.parent, true);

                // Match the speed with the player's speed
                SetSpeedBasedOnDollyCart();
            }
        }
    }

    public void SetSpeedBasedOnDollyCart()
    {
        // You can get the speed from the Dolly Cart script
        speed = dollyCart.m_Speed;
    }

    public void SetDestination(Transform destination)
    {
        this.destination = destination;
    }
}