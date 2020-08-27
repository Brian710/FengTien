﻿using UnityEngine;

public class SpoonController : IObjControllerBase
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
        goalType = Goal.Type.TakeSpoon;
    }
    public override void Start()
    {
        base.Start();
        ClickInteract.Iobj = this;
    }
    protected override void SetWaitingState()
    {
        ClickInteract.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        ClickInteract.enabled = true;
        transform.SetParent(startParent, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        transform.SetParent(targetParent, false);
        transform.localPosition = new Vector3(-0.00149f, 0.07244f, 0.0397000f);
        transform.localRotation = Quaternion.Euler(20.53f, 180f, 359.991f);
        PlayerController.Instance.RightHand.HandAnimChange(HandAnim.Spoon);
        ClickInteract.enabled = false;
        base.SetDoneState();
    }


    [SerializeField]
    private GameObject On;
    public bool IfHaveMat()
    {
        return On.activeSelf;
    }

    public void GetMat(bool value)
    {
        On.SetActive(value);
    }

}
