using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    private bool isActive = true;
    [SerializeField]
    private BoxCollider col;
    [SerializeField]
    List<GameObject> toDisappear = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            StartCoroutine(other.GetComponent<PlayerController>().Die());
        }
    }

    public void Desactivate(bool active)
    {
        isActive = active;

        if(isActive)
        {
            col.enabled = true;
            for (int i = 0; i < toDisappear.Count; i++)
            {
                toDisappear[i].SetActive(true);
            }
        }
        else
        {
            col.enabled = true;
            for (int i = 0; i < toDisappear.Count; i++)
            {
                toDisappear[i].SetActive(false);
            }
        }
        
    }
}
