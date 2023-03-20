using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private bool isInside;
    [SerializeField]
    Outline outline;


    private void Update()
    {
        if (isInside)
        {
            WaitForAction();
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

    private void WaitForAction()
    {
        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("canOpen", true);
            outline.enabled = false;
        }
    }
}


