using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinYanGame.Core;

public class SceneChangeTest : MonoBehaviour
{
    public static SceneChangeTest instance;
    public GameController gController;
    public float deletaTime = 10f;
    public bool ScenetestOver; 

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        gController = GameController.Instance;
        StartCoroutine(TestingSceneChange());
    }

    IEnumerator TestingSceneChange()
    {
        Debug.LogWarning("TestSceneStart");
        
        yield return new WaitForSeconds(deletaTime);
        gController.gameState = GameState.LevelInit;

        yield return new WaitForSeconds(deletaTime);
        gController.gameState = GameState.MainInit;

        Debug.LogWarning("TestSceneEnd");
    }
}
