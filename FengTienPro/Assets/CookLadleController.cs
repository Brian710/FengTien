using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookLadleController : IObjControllerBase ,IGrabbable
{
    [SerializeField]
    private GameObject On;
    [SerializeField]
    private BasicGrabbable _viveGrabFunc;
    [SerializeField]
    private HandAnim _handAnim;

    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;


    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.CookFood;
    }
    public override void Start()
    {
        base.Start();
        viveGrabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;
        viveGrabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        viveGrabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
        viveGrabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
    }
   
    public void HaveRice(bool value) => On.SetActive(value);

    public void OnGrab(bool value) => _viveGrabFunc.enabled = value;
    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        On.SetActive(false);
    }
    protected override void SetCurrentState()
    {
        viveGrabFunc.enabled = true;
    }
    protected override void SetDoneState()
    {
        viveGrabFunc.enabled = false;
    }
    public void GrabFunc_afterGrabberGrabbed()
    {
        PlayTakeSound();
    }

    public void GrabFunc_beforeGrabberReleased()
    {
    }
}
