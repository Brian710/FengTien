using System.Collections;
using UnityEngine;
using MinYanGame.Core;
using System.Collections.Generic;
using MEC;

[RequireComponent(typeof(PlayerController))]
public class LogoTrans : MonoBehaviour
{
    private void Start()
    {
        if(GameController.Instance.gameState == GameState.LogoInit)
           Timing.RunCoroutine(LogoInit());
    }

    private IEnumerator<float> LogoInit()
    {
        Debug.LogWarning("logoinit01");
        yield return 5f;
        Debug.LogWarning("logoinit02");
        GameController.Instance.gameState = GameState.StartInit;
    }
}



