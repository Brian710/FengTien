using System;
using UnityEngine;

public class BowlTempComtroller : MonoBehaviour
{
    [SerializeField]
    Goal.Type goalType;
    [SerializeField]
    Renderer render;
    [SerializeField]
    Collider colli;

    private void Start()
    {
        render.enabled = false;
        colli.enabled = false;
        QuestManager.Instance.GetQuestGoalByType(goalType).OnGoalStateChange += OnGoalStateChange;
    }
    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(goalType).OnGoalStateChange -= OnGoalStateChange;
    }
    private void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        if (type != goalType)
            return;

        switch (state)
        {
            case Goal.State.WAITING:
                SetWaitingState();
                break;
            case Goal.State.CURRENT:
                SetCurrentState();
                break;
            case Goal.State.DONE:
                SetDoneState();
                break;
        }
    }

    private void SetWaitingState()
    {
        render.enabled = false;
        colli.enabled = false;
    }
    private void SetCurrentState()
    {
        render.enabled = true;
        colli.enabled = true;
    }
    private void SetDoneState()
    {
        render.enabled = false;
        colli.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HandtemperController>())
        {
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
        }
    }
}
