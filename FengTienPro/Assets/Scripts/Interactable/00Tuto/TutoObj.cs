using HTC.UnityPlugin.Vive;
using UnityEngine;

public class TutoObj : IObjControllerBase, IGrabbable
{
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
    [SerializeField]
    private BasicGrabbable _viveGrabFunc;
    [SerializeField]
    private HandAnim _handAnim;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new HandAnim handAnim => _handAnim;

}
