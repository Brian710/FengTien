﻿using HTC.UnityPlugin.Vive;
using UnityEngine;

public class PestleController : IObjControllerBase, IGrabbable
{
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public GameObject Obj() => gameObject;
    [SerializeField] private GameObject Pestle;
    public override void Awake()
    {
        base.Awake();
        ChildObj.SetActive(false);
        goalType = Goal.Type.GrindMeds;
    }
    public override void Start()
    {
        if (viveGrabFunc == null)
            _viveGrabFunc = GetComponentInChildren<BasicGrabbable>();

        base.Start();
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
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
        Pestle.SetActive(false);
    }
}
