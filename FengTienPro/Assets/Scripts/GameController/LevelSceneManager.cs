using System.Collections;
using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{
    public GameObject TPObjects;
    public GameObject SelModeObj;

    public Transform MainScenePos;

    private void Start()
    {
        GameController.Instance.gameLevelnit += Set;
    }

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
        //StartCoroutine(PlayerController.Instance.TransAnimPlaytoEnd(true));
        GameController.Instance.gameState = GameState.MainInit;
        if (MainScenePos)
            StartCoroutine(PlayerController.Instance.ChangePos(MainScenePos));
    }

    private void OnDestroy()
    {
        GameController.Instance.gameLevelnit -= Set;
    }
}
