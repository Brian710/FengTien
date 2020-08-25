using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WashObj : IObjControllerBase, IWashable,IGrabbable
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
    public HandAnim handAnim => _handAnim;

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

    public void GrabFunc_beforeGrabberReleased()
    {
        if (ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Trigger))
        {
            PlayerController.Instance.EnableRightRay = true;
            PlayerController.Instance.RightHand.HandAnimChange(HandAnim.Normal);
        }
        else
        {
            PlayerController.Instance.EnableLeftRay = true;
            PlayerController.Instance.LeftHand.HandAnimChange(HandAnim.Normal);
        }
    }

    public void GrabFunc_afterGrabberGrabbed()
    {
        if (ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Trigger))
        {
            PlayerController.Instance.EnableRightRay = false;
            PlayerController.Instance.RightHand.HandAnimChange(handAnim);
        }
        else
        {
            PlayerController.Instance.EnableLeftRay = false;
            PlayerController.Instance.LeftHand.HandAnimChange(handAnim);
        }

        PlayTakeSound();
        hover.ShowInteractColor(false);
    }
}
