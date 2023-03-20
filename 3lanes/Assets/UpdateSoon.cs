using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateSoon : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private TMP_Text text;

    public float fadeDuration = 1f; // duration of fade in seconds
    private float currentAlpha = 0f; // current alpha value of component
    private float targetAlpha = 1f; // target alpha value of component
    private float timer = 0f; // timer for fading

    [SerializeField]
    private Material mat;
    private bool canFade;

    private void Start()
    {
        button.GetComponent<Image>().color = new Color(mat.color.r, mat.color.g, mat.color.b, currentAlpha);
        text.enabled = false;
        Debug.Log("Invoke");
        Invoke("ButtonAppear", 3.5f);
    }

    private void Update()
    {
        if (canFade)
        {
            timer += Time.deltaTime; // increment timer
            currentAlpha = Mathf.Lerp(0f, targetAlpha, timer / fadeDuration); // calculate current alpha value using linear interpolation
            button.GetComponent<Image>().color = new Color(mat.color.r, mat.color.g, mat.color.b, currentAlpha);

            if (currentAlpha >= targetAlpha) // if component has fully faded in
            {
                text.enabled = true;
                enabled = false; // disable this script
            }
        }
    }

    private void ButtonAppear()
    {
        canFade = true;
        Debug.Log("Invoke ended");
    }

    public void GoToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
