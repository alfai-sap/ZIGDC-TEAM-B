using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherCollector : MonoBehaviour
{
    public float flightDurationIncrease; // Amount to increase flight duration per feather

    private playerFlight playerFlight; // Reference to the PlayerFlight script

    private void Start()
    {
        playerFlight = FindObjectOfType<playerFlight>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Feather"))
        {
            CollectFeather(other.gameObject);
        }
    }

    private void CollectFeather(GameObject feather)
    {
        playerFlight.IncreaseFlightDuration(flightDurationIncrease);
        Destroy(feather);
    }
}
