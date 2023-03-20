using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject introCamera;
    [SerializeField]
    private float animationTime;
    [SerializeField]
    private float timeBeforeSpawn;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject spawnPoint;
    public GameObject playerSpawned;

    private void Start()
    {
        introCamera.SetActive(true);
        Invoke("StopIntro", animationTime);
        Invoke("SpawnPlayer", timeBeforeSpawn);
    }

    private void SpawnPlayer()
    {
        playerSpawned = Instantiate(player, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private void StopIntro()
    {
        introCamera.GetComponent<Camera>().enabled = false;
        playerSpawned.GetComponent<PlayerController>().canMove = true;
    }
}
