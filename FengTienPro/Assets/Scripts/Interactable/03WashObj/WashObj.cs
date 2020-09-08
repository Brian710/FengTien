using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WashObj : IObjControllerBase, IWashable, IGrabbable
{
    [SerializeField]    private bool isWashed;
    [SerializeField]    private int washTime;
    [SerializeField]    private HandAnim _handAnim;
    [SerializeField]    private Goal.Type type;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new  HandAnim handAnim => _handAnim;
    public GameObject Obj() => gameObject;
    public int WashTime() => washTime;

    public bool IsWashed() => isWashed;
    public override void Awake()
    {
        base.Awake();
        goalType = type;
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

    public void SetWashed(bool value)
    {
        isWashed = value;
        //Show UI Completed
        if(isWashed)    PlayerController.Instance.QuestStepCompleted();
    }
    
    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        SetWashed(false);
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
