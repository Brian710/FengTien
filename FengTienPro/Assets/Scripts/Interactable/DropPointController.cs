using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointController : CheckPointBase
{
    public MultiSpawnObjOnTriggerExit resetObject;

    [SerializeField] private WashObj WashedObj;
    [SerializeField] private GameObject ChildObj;
    public override void Start()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashObj).OnGoalStateChange += OnGoalStateChange;
        ShowParticle(true);
    }


    public void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                FX.Stop(true);
                ChildObj.SetActive(false);
                break;
            case Goal.State.CURRENT:
                FX.Play(true);
                ChildObj.SetActive(true);
                break;
            case Goal.State.DONE:
                FX.Stop(true);
                ChildObj.SetActive(false);
                break;

        }
    
    }
    public override void OnTriggerEnter(Collider other)
    {
        WashedObj = other.gameObject.GetComponent<WashObj>();

        if (WashedObj != null)
        {
            QuestManager.Instance.AddQuestCurrentAmount(WashedObj.goalType);
            onTriggerEnter.Invoke();
        }
        //else
        //{
        //    if (GameController.Instance.mode == MainMode.Exam)
        //    {
        //        other.GetComponent<IObjControllerBase>().ShowError();
        //        QuestManager.Instance.MinusQuestScore(2);
        //    }
        //    resetObject.ForcetoReset(other.gameObject);
        //}
    }
}
