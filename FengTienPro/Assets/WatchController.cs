using UnityEngine;

public class WatchController : IObjControllerBase
{
    [SerializeField]
    private ClicktoInteract ClickInteract;
    [SerializeField]
    private Transform startParent;
    [SerializeField]
    private Transform targetParent;

    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.Watch;
    }
    public override void Start()
    {
        base.Start();
        ClickInteract.Iobj = this;
    }
    protected override void SetWaitingState()
    {
        transform.SetParent(startParent, false);
        transform.position = startParent.position;
        transform.rotation = startParent.rotation;
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
        transform.SetParent(targetParent, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        ClickInteract.enabled = false;
        base.SetDoneState();
    }

}
