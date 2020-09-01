using UnityEngine;

public class WatchController : IObjControllerBase
{
    [SerializeField]    private ClicktoInteract ClickInteract;
    [SerializeField]    private Transform startParent;
    [SerializeField]    private Transform targetParent;

    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.Watch;
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
        transform.SetParent(startParent, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
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
