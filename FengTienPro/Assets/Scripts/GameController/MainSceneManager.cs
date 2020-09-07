using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]    private List<GameObject> gameObjects;
    [SerializeField]    private Transform StartScenePos;

    private void Start()
    {
        MainSceneStartInorder();
    }
    private void OnDestroy()
    {
    }
    public void MainSceneStartInorder()
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

        GameController.Instance.gameState = GameState.StartInit;
        StartCoroutine(PlayerController.Instance.TransAnimPlaytoEnd(true));

        if (StartScenePos)
            StartCoroutine(PlayerController.Instance.ChangePos(StartScenePos));
    }
}
