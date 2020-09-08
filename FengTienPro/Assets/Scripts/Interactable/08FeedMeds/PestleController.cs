using HTC.UnityPlugin.Vive;
using UnityEngine;

public class PestleController : IObjControllerBase, IGrabbable
{
    [SerializeField] private HandAnim _handAnim;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new HandAnim handAnim => _handAnim;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.GrindMeds;
    }

    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        viveGrabFunc.enabled = true;
    }
    protected override void SetDoneState()
    {
        viveGrabFunc.enabled = false;
        ChildObj.transform.position = position;
        ChildObj.transform.rotation = rotation;
    }
}
