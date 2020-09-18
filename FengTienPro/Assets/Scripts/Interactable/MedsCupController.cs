using HTC.UnityPlugin.Vive;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MedsCupController : IObjControllerBase, IGrabbable
{
    [SerializeField] private Animator Anim;
    [SerializeField] private Transform BowlTrans;
    [SerializeField] private Transform WaterTrans;
    [SerializeField] private GameController bowl;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public bool MixWaterMeds;

    public override void Start()
    {
        MixWaterMeds = false;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.PourPowder).OnGoalStateChange += OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.MixWater).OnGoalStateChange += OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).OnGoalStateChange += OnGoalStateChange;
    }
    public override void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.PourPowder).OnGoalStateChange -= OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.MixWater).OnGoalStateChange -= OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).OnGoalStateChange -= OnGoalStateChange;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<BowlPillController>() && QuestManager.Instance.GetQuestGoalByType(Goal.Type.PourPowder).state == Goal.State.CURRENT)
        {
            Anim.SetBool("Pour", true);
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.PourPowder);
            Debug.LogError("碗中藥倒進藥杯");
        }
       else if (other.GetComponentInParent<MedsWaterController>() && QuestManager.Instance.GetQuestGoalByType(Goal.Type.MixWater).state == Goal.State.CURRENT)
       {
            Anim.SetBool("MixWater", true);
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.MixWater);
            Debug.LogError("水倒進藥杯");
       }
       if (other.GetComponentInParent<TubeController>() && QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).state == Goal.State.CURRENT)
       {
            Anim.SetFloat("WaterDown", 1f);
            MixWaterMeds = true;
            Debug.LogError("藥倒進針筒");
       }
    }
    protected override void SetWaitingState()
    {
        hover.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        hover.enabled = true;
        hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
    }
    protected override void SetDoneState()
    {
        hover.enabled = false;
    }
}
