using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlightDurationBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxFlightDuration(float duration)
    {
        slider.maxValue = duration;
        slider.value = duration;
    }

    public void SetFlightDuration(float duration)
    {
        slider.value = duration;
    }
}
