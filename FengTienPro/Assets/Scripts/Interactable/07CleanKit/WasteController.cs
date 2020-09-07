using UnityEngine;
public class WasteController : IObjControllerBase
{
    [SerializeField] private Goal.Type type;
    [SerializeField] private Collider colli;

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
    }
    private bool PotIn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<WasteObj>())
        {
            PotIn = true;
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<WasteObj>())
            PotIn = false;
    }
}
