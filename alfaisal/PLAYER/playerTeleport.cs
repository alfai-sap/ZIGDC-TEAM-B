using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTeleport : MonoBehaviour
{
    public Transform portalA; // Reference to the first portal
    public Transform portalB; // Reference to the second portal
    public KeyCode teleportKey = KeyCode.T; // Key to trigger teleportation

    private bool isNearPortalA = false; // Is the player near portal A
    private bool isNearPortalB = false; // Is the player near portal B

    private void Update()
    {
        if (isNearPortalA && Input.GetKeyDown(teleportKey))
        {
            Teleport(portalB);
        }
        else if (isNearPortalB && Input.GetKeyDown(teleportKey))
        {
            Teleport(portalA);
        }
    }

    private void Teleport(Transform targetPortal)
    {
        transform.position = targetPortal.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == portalA)
        {
            isNearPortalA = true;
        }
        else if (other.transform == portalB)
        {
            isNearPortalB = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == portalA)
        {
            isNearPortalA = false;
        }
        else if (other.transform == portalB)
        {
            isNearPortalB = false;
        }
    }
}
