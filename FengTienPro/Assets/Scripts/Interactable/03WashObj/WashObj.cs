using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WashObj : IObjControllerBase, IWashable, IGrabbable
{
    [SerializeField]
    private bool isWashed;
    [SerializeField]
    private int washTime;
    [SerializeField]
    private BasicGrabbable _viveGrabFunc;
    [SerializeField]
    private HandAnim _handAnim;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new  HandAnim handAnim => _handAnim;
    public GameObject Obj() => this.gameObject;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.WashObj;
    }

    public override void Start()
    {
        base.Start();
        viveGrabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        viveGrabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
    }

    public bool IsWashed(bool value)
    {
        if (isWashed == value)
            return isWashed;

        isWashed = value;

        if(isWashed)
            PlayerController.Instance.QuestStepCompleted();

        return isWashed;
    }
    public int WashTime() => washTime;
    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        isWashed = false;
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
        base.SetDoneState();
    }
}
