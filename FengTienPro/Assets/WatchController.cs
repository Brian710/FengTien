using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchController : ClicktoPosBase
{
    [SerializeField]
    private Transform parent;

    private void Start()
    {
        ObjOriginPosition = transform.localPosition;
        ObjOriginRotation = transform.localRotation;
    }
    public override void Set()
    {
        base.Set();
        transform.SetParent(parent, false);
        transform.localPosition = ObjOriginPosition;
        transform.localRotation = ObjOriginRotation;
        ShowHintColor(true);
    }
}
