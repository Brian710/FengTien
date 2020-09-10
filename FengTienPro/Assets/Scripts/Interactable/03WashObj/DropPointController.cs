﻿using UnityEngine;

public class DropPointController : CheckPointBase
{
    [SerializeField] private WashObj WashedObj;
    [SerializeField] private GameObject ChildObj;
    public override void Start()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashObj).OnGoalStateChange += OnGoalStateChange;
        ChildObj.SetActive(false);
    }

    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashObj).OnGoalStateChange -= OnGoalStateChange;
    }
    public void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                ShowParticle(false);
                ChildObj.SetActive(false);
                break;
            case Goal.State.CURRENT:
                ShowParticle(true);
                ChildObj.SetActive(true);
                break;
            case Goal.State.DONE:
                ShowParticle(false);
                ChildObj.SetActive(false);
                break;
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        WashedObj = other.gameObject.GetComponentInParent<WashObj>();
        if (WashedObj)
        {
            if (WashedObj.IsWashed()&& !WashedObj.isDry)
            {
                QuestManager.Instance.AddQuestCurrentAmount(WashedObj.goalType);
                Debug.LogError("洗好一個");
                onTriggerEnter.Invoke();
                WashedObj.viveGrabFunc.enabled = false;
                //WashedObj.SetChildObjActive(false);
                WashedObj.isDry = true;
            }
            else
            {
                QuestManager.Instance.MinusQuestScore(1);
                WashedObj.ShowError();
            }
        }
    }
}
