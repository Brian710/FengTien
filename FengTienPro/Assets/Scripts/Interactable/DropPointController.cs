using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointController : CheckPointBase
{
    public MultiSpawnObjOnTriggerExit resetObject;

    [SerializeField] private WashObj WashedObj;
    public override void Start()
    {
        ShowParticle(true);
    }

    public override void OnTriggerEnter(Collider other)
    {
        WashedObj = other.gameObject.GetComponent<WashObj>();
        if (WashedObj != null)
        {
            QuestManager.Instance.AddQuestCurrentAmount(WashedObj.goalType);
            WashedObj.SetGrabble(false);
            onTriggerEnter.Invoke();
        }
        else
        {
            if (GameController.Instance.mode == MainMode.Exam)
            {
                other.GetComponent<InteractableObjBase>().ShowError();
                QuestManager.Instance.MinusQuestScore(2);
            }
            resetObject.ForcetoReset(other.gameObject);
        }
    }
}
