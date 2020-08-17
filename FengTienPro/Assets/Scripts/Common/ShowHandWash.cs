using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHandWash : MonoBehaviour
{
    public  Animator HandWashing;

    private void OnEnable()
    {
        ShowAnimation(true);
    }

    private void OnDisable()
    {
        ShowAnimation(false);
    }

    public void ShowAnimation(bool value)
    {
        if (GameController.Instance.mode == MainMode.Exam)
        {
            HandWashing.SetBool("AutoShow", false);
            return;
        }

        HandWashing.SetBool("AutoShow", value);
    }

}
