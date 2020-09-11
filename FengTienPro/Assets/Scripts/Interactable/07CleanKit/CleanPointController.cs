using UnityEngine;

public class CleanPointController : CheckPointBase
{
    [SerializeField] private WashObj WashedObj;
    [SerializeField] private GameObject ChildObj;
    [SerializeField] private Collider CleanPoint;

    public override void Start()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashStuff).OnGoalStateChange += OnGoalStateChange;
        ChildObj.SetActive(false);
        CleanPoint.enabled = false;
    }

    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashStuff).OnGoalStateChange -= OnGoalStateChange;
    }
    public void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                ShowParticle(false);
                ChildObj.SetActive(false);
                CleanPoint.enabled = false;
                break;
            case Goal.State.CURRENT:
                ShowParticle(true);
                ChildObj.SetActive(true);
                CleanPoint.enabled = true;
                break;
            case Goal.State.DONE:
                ShowParticle(false);
                ChildObj.SetActive(false);
                CleanPoint.enabled = false;
                break;
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        WashedObj = other.gameObject.GetComponentInParent<WashObj>();
        if (WashedObj)
        {
            if (WashedObj.IsWashed() && !WashedObj.isDry)
            {
                QuestManager.Instance.AddQuestCurrentAmount(WashedObj.goalType);
                onTriggerEnter.Invoke();
                WashedObj.viveGrabFunc.enabled = false;
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
