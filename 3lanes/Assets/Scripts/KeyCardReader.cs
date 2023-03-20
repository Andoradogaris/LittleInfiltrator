using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardReader : MonoBehaviour
{
    //[HideInInspector]
    public bool gotCard;
    [SerializeField]
    private OpenDoor openDoor;
    private bool isInside;
    [SerializeField]
    private Outline outline;

    private void Update()
    {
        if (isInside)
        {
            if (gotCard && Input.GetKeyDown(KeyCode.E))
            {
                openDoor.canUse = true;
                outline.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }
}
