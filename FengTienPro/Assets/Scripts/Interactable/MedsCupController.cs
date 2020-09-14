using HTC.UnityPlugin.Vive;
using UnityEngine;

public class MedsCupController : IObjControllerBase, IGrabbable
{
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject ON;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;

    public override void Start()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.PourPowder).OnGoalStateChange += OnPourPowderChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.MixWater).OnGoalStateChange += OnMixWaterChange;
        SetChildObjActive(false);
    }

    public override void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.PourPowder).OnGoalStateChange -= OnPourPowderChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.MixWater).OnGoalStateChange -= OnMixWaterChange;
    }
    public override void Awake()
    {
        base.Awake();
        //goalType = Goal.Type.MixWater;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<BowlPillController>())
        {
            Anim.SetBool("Pour", true);
        }
        if (other.GetComponentInParent<FeedWaterController>() && other.GetComponentInParent<FeedWaterController>().goalType == Goal.Type.MixWater)
        {
            Anim.SetBool("Pour", true);
        }

    }
    protected void OnPourPowderChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                hover.enabled = false;
                break;
            case Goal.State.CURRENT:
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Goal.State.DONE:
                hover.enabled = false;
                break;
        }
    }
    protected void OnMixWaterChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                hover.enabled = false;
                break;
            case Goal.State.CURRENT:
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                ON.SetActive(false);
                break;
            case Goal.State.DONE:
                hover.enabled = false;
                ON.SetActive(true);
                break;
        }
    }
}
