using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointController : CheckPointBase
{
    public MultiSpawnObjOnTriggerExit resetObject;
    public override void Start()
    {
        ShowParticle(true);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InteractableObjBase>().goalType == Goal.Type.WashObj)
        {
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.WashObj);
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
