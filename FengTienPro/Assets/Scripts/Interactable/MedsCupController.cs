using HTC.UnityPlugin.Vive;
using UnityEngine;

public class MedsCupController : IObjControllerBase, IGrabbable
{
    [SerializeField] private Animator Anim;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;

    public override void Start()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.PourPowder).OnGoalStateChange += OnPourPowderChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.MixWater).OnGoalStateChange += OnMixWaterChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).OnGoalStateChange += OnFeedMedsChange;
        SetChildObjActive(false);
    }

    public override void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.PourPowder).OnGoalStateChange -= OnPourPowderChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.MixWater).OnGoalStateChange -= OnMixWaterChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).OnGoalStateChange -= OnFeedMedsChange;
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
       else if (other.GetComponentInParent<FeedWaterController>() && other.GetComponentInParent<FeedWaterController>().goalType == Goal.Type.MixWater)
        {
            Anim.SetBool("MixWater", true);
        }
        else if (other.GetComponentInParent<TubeController>() && other.GetComponentInParent<FeedWaterController>().goalType == Goal.Type.FeedMeds)
        {
            Anim.SetFloat("WaterDown", Time.deltaTime * 0.1f);
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
                break;
            case Goal.State.DONE:
                hover.enabled = false;
                break;
        }
    }
    protected void OnFeedMedsChange(Goal.Type type, Goal.State state)
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
}
