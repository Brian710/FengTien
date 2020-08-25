using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualObjTwo : VirtualObj
{
    public override void VirtualObjVOFunc()
    {
        base.VirtualObjVOFunc();
        Debug.Log("two");
    }
}
