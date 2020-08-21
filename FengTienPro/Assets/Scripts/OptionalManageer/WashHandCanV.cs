using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WashHandCanV : OptionalSystemBase
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    public override void OpenCanv(bool value)
    {
        if (value)
        {
            if (canvasGroup && QuestManager.Instance.GetcurrentGoal() == Goal.Type.WashHandCanv)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
            }
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }
    }
}
