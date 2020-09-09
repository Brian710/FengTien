using HTC.UnityPlugin.Vive;
using UnityEngine;

public class GetRagController : IObjControllerBase
{
    [SerializeField] private ClicktoInteract ClickInteract;
    [SerializeField] public  GameObject GetRag;
    [SerializeField] private Transform targetParent;
    [SerializeField] private Collider colli;
    public bool RagInHand;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.None;
        RagInHand = false;
        hover.enabled = false;
    }
    public override void Start()
    {
        ClickInteract.IObj = this;
        ChildObj.SetActive(false);
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.CleanDesk).OnGoalStateChange += OnGoalStateChange;
    }
    public override void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.CleanDesk).OnGoalStateChange -= OnGoalStateChange;
    }
    public override void SetChildObjActive(bool value)
    {
        ChildObj.SetActive(value);
        if (value)
            SetWaitingState();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<VivePoseTracker>())
        {
            GetRag.SetActive(true);
            GetRag.transform.SetParent(targetParent, false);
            GetRag.transform.localPosition = new Vector3(-0.008f, 0, 0.11f);
            GetRag.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
            ClickInteract.enabled = true;
            RagInHand = true;
        }
    }
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        SetChildObjActive(false);
        colli.enabled = false;
    }
    protected override void SetCurrentState()
    {
        SetChildObjActive(true);
        colli.enabled = true;
    }
    protected override void SetDoneState()
    {
        GetRag.SetActive(false);
        colli.enabled = false;
    }
}
