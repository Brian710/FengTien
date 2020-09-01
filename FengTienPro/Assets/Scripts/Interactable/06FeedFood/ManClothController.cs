using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManClothController : IObjControllerBase
{
    [SerializeField]
    private ClicktoInteract ClickInteract;
    [SerializeField]
    private Transform startParent;
    [SerializeField]
    private Transform targetParent;

    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.PutOnBib;
    }
    public override void Start()
    {
        base.Start();
        ClickInteract.IObj = this;
    }
    protected override void SetWaitingState()
    {
        ClickInteract.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        ClickInteract.enabled = true;
        transform.position = position;
        transform.rotation = rotation;
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        transform.SetParent(targetParent, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        ClickInteract.enabled = false;
        base.SetDoneState();
    }
}
