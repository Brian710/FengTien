using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WasteObj : IObjControllerBase, IGrabbable
{
    //[SerializeField] private bool isWashed;
    //[SerializeField] private int washTime;
    [SerializeField] private HandAnim _handAnim;
    [SerializeField] private Goal.Type type;
    [SerializeField] private GameObject Waste;

    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new HandAnim handAnim => _handAnim;
    public GameObject Obj() => gameObject;
    public override void Awake()
    {
        base.Awake();
        ChildObj.SetActive(false);
        hover.InteractColor = new Color(0, .74f, .74f, 1);
        hover.hintColor = new Color(1, 0.8f, .28f, 1);
    }
    public override void Start()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.ThrowWaste).OnGoalStateChange += OnGoalStateChange;
        hover.enabled = false;
    }
    public override void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.ThrowWaste).OnGoalStateChange += OnGoalStateChange;
    }
    private void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                SetWaitingState();
                hover.enabled = false;
                Waste.SetActive(true);
                break;
            case Goal.State.CURRENT:
                SetCurrentState();
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Goal.State.DONE:
                SetDoneState();
                Waste.SetActive(false);
                hover.enabled = false;
                break;
        }
    }
}
