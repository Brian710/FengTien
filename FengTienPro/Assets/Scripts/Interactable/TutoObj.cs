using HTC.UnityPlugin.Vive;
using UnityEngine;

public class TutoObj : InteracObjBase,IGrabbable
{
    public override void Start()
    {
        base.Start();
        viveGrabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;
    }
    private void OnDestroy()
    {
        viveGrabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
    }
    public BasicGrabbable viveGrabFunc { get; set; }
    public HandAnim handAnim { get; set; }

    
    public override void InteractInvoke(bool value)
    {
    }

    public void GrabFunc_afterGrabberGrabbed()
    {
        PlayTakeSound();
    }

    public void GrabFunc_beforeGrabberReleased()
    {
        throw new System.NotImplementedException();
    }

    
}
