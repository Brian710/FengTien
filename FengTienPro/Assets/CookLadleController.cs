using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookLadleController : InteractableObjBase
{
    [SerializeField]
    private GameObject On;

    public override void Set()
    {
        base.Set();
        
        if (grabFunc == null)
            grabFunc = GetComponent<BasicGrabbable>();

        HaveRice(false);
    }


    public void HaveRice(bool value)
    {
        On.SetActive(value);
        grabFunc.enabled = value;
    }
}
