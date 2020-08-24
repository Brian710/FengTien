using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gameObjects;

    [SerializeField]
    private Transform StartScenePos;

    private void Start()
    {
        GameController.Instance.gameMainInit += Set;
    }
    public void Set()
    {
        if (gameObjects.Count <= 0)
            return;
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(true);
        }
    }

    public void BacktoStart()
    {
        if (gameObjects.Count <= 0)
            return;

        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }

        GameController.Instance.gameState = GameState.StartInit;
        StartCoroutine(PlayerController.instance.TransAnimPlaytoEnd(true));
        if (StartScenePos)
            StartCoroutine(PlayerController.instance.ChangePos(StartScenePos));
    }

    private void OnDestroy() => GameController.Instance.gameMainInit -= Set;
}
