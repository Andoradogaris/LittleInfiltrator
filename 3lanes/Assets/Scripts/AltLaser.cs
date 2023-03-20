using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltLaser : MonoBehaviour
{
    [SerializeField]
    private GameObject right;
    [SerializeField]
    private GameObject left;
    [SerializeField]
    private float timer;

    void Start()
    {
        StartCoroutine(StartAlt());
    }

    IEnumerator StartAlt()
    {
        right.SetActive(true);
        left.SetActive(false);
        while (true)
        {
            if (right.activeInHierarchy)
            {
                right.SetActive(false);
                left.SetActive(true);
            }
            else if (left.activeInHierarchy)
            {
                right.SetActive(true);
                left.SetActive(false);
            }
            yield return new WaitForSeconds(timer);
        }
    }
}
