using HTC.UnityPlugin.Vive;
using UnityEngine;

public class TutoObj : IObjControllerBase, IGrabbable
{
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.Tuto;
    }
    public override void Start()
    {
        SetChildObjActive(true);
        viveGrabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;
    }
    public override void OnDestroy()
    {
        viveGrabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
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
        SetChildObjActive(false);
    }
    protected override void SetCurrentState()
    {
        SetChildObjActive(true);
    }
}
