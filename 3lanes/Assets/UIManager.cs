using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
