using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    [SerializeField]
    private bool canGet;
    [SerializeField]
    private KeyCardReader keyCardReader;

    private void Update()
    {
        if (canGet && Input.GetKeyDown(KeyCode.E))
        {
            keyCardReader.gotCard = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canGet = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canGet = false;
        }
    }
}
