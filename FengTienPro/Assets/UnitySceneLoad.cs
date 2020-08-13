using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitySceneLoad:MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void LoadTutoScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void LoadMainScene()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
