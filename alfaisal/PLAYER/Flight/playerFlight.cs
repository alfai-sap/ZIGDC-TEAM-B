using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerFlight : MonoBehaviour
{
    public float flightForce;
    public float flightDuration;
    private float flightTimeCounter;
    public float maxFlightDuration;

    private Rigidbody2D rb;
    public Slider flightDurationBar; // Reference to the Slider UI

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flightTimeCounter = flightDuration;
        flightDurationBar.maxValue = flightDuration;
        flightDurationBar.value = flightTimeCounter;

        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && flightTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, flightForce);
            flightTimeCounter -= Time.deltaTime;
            flightDurationBar.value = flightTimeCounter;
        }
    }

    public void IncreaseFlightDuration(float amount)
    {
        flightDuration += amount;
        flightTimeCounter += amount;
        flightDurationBar.maxValue = flightDuration; // Update the max value of the slider
        flightDurationBar.value = flightTimeCounter; // Update the current value of the slider
    }

    
}
