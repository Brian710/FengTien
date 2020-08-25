using HTC.UnityPlugin.Vive;
using UnityEngine;

public class TutoObj : IObjControllerBase,IGrabbable
{
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.Tuto;
    }

    public override void Start()
    {
        SetInterObjActive(false);
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
    public HandAnim handAnim => _handAnim;
    
    public override void InteractInvoke(bool value)
    {
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
