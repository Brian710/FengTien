using UnityEngine;
public class WasteController : IObjControllerBase
{
    [SerializeField] private Collider colli;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.ThrowWaste;
        hover.enabled = false;
    }
    public override void Start()
    {
        base.Start();
    }
    public override void SetChildObjActive(bool value)
    {
        ChildObj.SetActive(value);
        if (value)
            SetWaitingState();
    }
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        colli.enabled = false;
    }
    protected override void SetCurrentState()
    {
        colli.enabled = true;
        //SetChildObjActive(true);
    }
    protected override void SetDoneState()
    {
        colli.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<WasteObj>())
        {
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
        }
    }
}
