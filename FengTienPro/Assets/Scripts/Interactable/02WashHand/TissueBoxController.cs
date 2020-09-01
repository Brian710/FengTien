using UnityEngine;
public class TissueBoxController : IObjControllerBase
{
    [SerializeField]    private ClicktoInteract ClickInteract;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.Tissue;
    }
    public override void Start()
    {
        if (ClickInteract == null)
            ClickInteract = GetComponentInChildren<ClicktoInteract>();
        ClickInteract.IObj = this;
        base.Start();
    }
    protected override void SetWaitingState()
    {
        ClickInteract.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        ClickInteract.enabled = true;
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        ClickInteract.enabled = false;
        base.SetDoneState();
    }
}
