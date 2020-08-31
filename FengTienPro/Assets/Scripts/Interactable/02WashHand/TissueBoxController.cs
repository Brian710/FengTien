using UnityEngine;
public class TissueBoxController : IObjControllerBase
{
    [SerializeField]
    private ClicktoInteract ClickInteract;
    public override void Start()
    {
        goalType = Goal.Type.Tissue;
        base.Start();
        ClickInteract.Iobj = this;
        ClickInteract.enabled = false;
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
