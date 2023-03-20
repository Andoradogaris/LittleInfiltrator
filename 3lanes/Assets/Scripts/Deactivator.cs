using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    [SerializeField]
    List<GameObject> traps = new List<GameObject>();
    [SerializeField]
    private bool isWaiting;
    [SerializeField]
    private float deactivateTime;
    [SerializeField]
    private Outline outline;

    private bool activateAgain;
    private bool isInside;

    private void Start()
    {
        GameObject[] trapsArray = GameObject.FindGameObjectsWithTag("Trap");
        foreach (GameObject trap in trapsArray)
        {
            traps.Add(trap);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tag == "WallDeactivator")
            {
                isWaiting = true;
                StartCoroutine(WaitForAction());
            }
            else
            {
                for (int i = 0; i < traps.Count; i++)
                {
                    traps[i].GetComponent<TrapManager>().Desactivate(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInside = false;
        if (tag == "WallDeactivator")
        {
            isWaiting = false;
        }
        if(activateAgain == false)
        {
            StartCoroutine(ActivateAgain());
        }
    }

    IEnumerator WaitForAction()
    {
        while (isWaiting)
        {
            if (Input.GetKey(KeyCode.E))
            {
                outline.enabled = false;
                for (int i = 0; i < traps.Count; i++)
                {
                    traps[i].GetComponent<TrapManager>().Desactivate(false);
                }
                isWaiting = false;
                break;
            }
            yield return null;
            activateAgain = true;
            StartCoroutine(ActivateAgain());
        }
    }

    IEnumerator ActivateAgain()
    {
        yield return new WaitForSeconds(deactivateTime);
        outline.enabled = true;
        activateAgain = true;
        for (int i = 0; i < traps.Count; i++)
        {
            traps[i].GetComponent<TrapManager>().Desactivate(true);
        }
    }
}
