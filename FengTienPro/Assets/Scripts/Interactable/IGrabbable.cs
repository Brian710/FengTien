using HTC.UnityPlugin.Vive;

public interface IGrabbable
{
    BasicGrabbable viveGrabFunc { get;} 

    HandAnim handAnim { get; }

    void GrabFunc_afterGrabberGrabbed();

    void GrabFunc_beforeGrabberReleased();
}
