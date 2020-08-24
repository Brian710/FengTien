using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    BasicGrabbable viveGrabFunc { get; set; } 

    HandAnim handAnim { get; set; }

    void GrabFunc_afterGrabberGrabbed();

    void GrabFunc_beforeGrabberReleased();
}
