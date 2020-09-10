using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WasteObj : IObjControllerBase, IGrabbable
{
    [SerializeField] private GameObject Waste;

    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public GameObject Obj() => gameObject;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.ThrowWaste;
    }
    public override void Start()
    {
        if (viveGrabFunc == null)
            _viveGrabFunc = GetComponentInChildren<BasicGrabbable>();

        viveGrabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;
        base.Start();
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        viveGrabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
    }
    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        viveGrabFunc.enabled = true;
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        viveGrabFunc.enabled = false;
        Waste.SetActive(false);
        base.SetDoneState();
    }
}
