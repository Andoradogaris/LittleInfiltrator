using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLevel : MonoBehaviour
{

    [SerializeField]
    private GameObject faddingObj;
    [SerializeField]
    private TMP_Text endText;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private float fadeDuration = 1f;
    private Material material;
    private float fadeTime;
    private bool canFade;

    private void Start()
    {
        Renderer renderer = faddingObj.GetComponent<Renderer>();
        material = renderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.canMove = false;
            canFade = true;
        }
    }


    void Update()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (canFade)
        {
            // Update the fade time based on the time elapsed since the last frame
            fadeTime += Time.deltaTime;

            // Calculate the current alpha value based on the fade time and duration
            float alpha = Mathf.Clamp01(fadeTime / fadeDuration);

            // Set the alpha value of the material's color
            Color color = material.color;
            color.a = alpha;
            material.color = color;

            // Disable the script if the fade is complete
            if (fadeTime >= fadeDuration)
            {
                enabled = false;
            }
        }
    }
}


