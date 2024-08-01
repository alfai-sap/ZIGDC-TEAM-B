using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeatherCounter : MonoBehaviour
{
    public TextMeshProUGUI featherText;
    private int featherCount = 0;

    public void IncreaseFeatherCount()
    {
        featherCount++;
        featherText.text = "Feathers: " + featherCount.ToString();
    }
}
