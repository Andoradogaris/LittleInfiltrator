using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private bool isInside;
    [HideInInspector]
    public bool canUse;
    [SerializeField]
    private Outline outline;

    private void Start()
    {
        outline.enabled = false;
    }

    private void Update()
    {
        if (canUse)
        {
            outline.enabled = true;
            if (isInside && Input.GetKeyDown(KeyCode.E))
            {
                outline.enabled = false;
                anim.SetBool("canOpen", true);
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
}
