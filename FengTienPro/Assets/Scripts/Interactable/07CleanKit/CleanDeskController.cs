using HTC.UnityPlugin.Vive;
using UnityEngine;

public class CleanDeskController : IObjControllerBase
{
    [SerializeField] private Goal.Type type;
    [SerializeField] private Collider colli;
    [SerializeField] private GameObject Rag;

    public override void Awake()
    {
        base.Awake();
        goalType = type;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void SetChildObjActive(bool value)
    {
        if (value)
        {
            SetWaitingState();
        }
        else
        {
            ChildObj.SetActive(true);
        }
    }
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        colli.enabled = false;
    }

    protected override void SetCurrentState()
    {
        colli.enabled = true;
    }

    protected override void SetDoneState()
    {
        colli.enabled = false;
        Rag.SetActive(false);
    }
    private bool RagIn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<VivePoseTracker>())
        {
            Debug.LogError("擦桌子囉!");
            RagIn = true;
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<GetRagController>())
            RagIn = false;
    }
}
