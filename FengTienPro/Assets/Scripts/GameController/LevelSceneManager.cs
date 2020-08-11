using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{
    public GameObject TPObjects;
    public GameObject SelModeObj;

    public Transform MainScenePos;

    public void Set()
    {
        SelModeObj.SetActive(true);
        TPObjects.SetActive(false);
    }

    public void SetTrainModeorNot(bool value)
    {
        GameController.Instance.mode = value ? MainMode.Train : MainMode.Exam;
        TPObjects.SetActive(true);
        SelModeObj.SetActive(false);
    }

    public void SetTPsInactive()
    {
        StartCoroutine(SetTPsInactiveCo());
    }

    private IEnumerator SetTPsInactiveCo()
    {
        yield return new WaitForSeconds(5f);
        TPObjects.SetActive(false);
    }

    public void ChangePostoMain()
    {
        GameController.Instance.gameState = GameState.MainInit;
        StartCoroutine(PlayerController.instance.TransAnimPlaytoEnd(true));
        if (MainScenePos)
            StartCoroutine(PlayerController.instance.ChangePos(MainScenePos));
    }
}
