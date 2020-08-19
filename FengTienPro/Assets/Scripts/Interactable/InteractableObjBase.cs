﻿using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
public class InteractableObjBase : MonoBehaviour
        , IColliderEventHoverEnterHandler
        , IColliderEventHoverExitHandler
{

    public Goal.Type goalType;

    [SerializeField]
    protected HandAnim handAnim;

    [SerializeField]
    private bool FirstInit = false;

    [SerializeField]
    protected QuickOutline outline;

    [SerializeField]
    private BasicGrabbable grabFunc;

    public Color InteractColor = new Color(0, .74f, .74f, 1);
    public Color hintColor = new Color(1, 0.8f, .28f, 1); // quick outline default hint color

    public UnityEvent afteInteract;

    private void Start()
    {
        if (outline)
        {
            Debug.LogWarning("outline Start");
            outline.OutlineColor = hintColor;
            outline.enabled = false;
        }
        Set();
        FirstInit = true;
    }

    private void OnEnable()
    {
        Set();
    }

    private void OnDisable()
    {
        Remove();
    }

    public virtual void Set()
    {
        if (grabFunc == null)
            grabFunc = GetComponent<BasicGrabbable>();

        grabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
        grabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;

    }

    private void GrabFunc_afterGrabberGrabbed()
    {
        PlayerController.instance.RightHand.HandAnimChange(handAnim);
    }
    private void GrabFunc_beforeGrabberReleased()
    {
        PlayerController.instance.RightHand.HandAnimChange(HandAnim.Normal);
    }

    public virtual void Remove()
    {
        grabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
        grabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
    }

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData) => ShowInteractColor(true);

    public void OnColliderEventHoverExit(ColliderHoverEventData eventData) => ShowInteractColor(false);

    public virtual void ShowInteractColor(bool value) => ShowOutline(value, InteractColor);

    public virtual void ShowHintColor(bool value)
    {
        if (GameController.Instance.mode == MainMode.Train&& GameController.Instance.currentGoal.type == goalType)
        { 
            ShowOutline(value, hintColor);
        }
    }

    public virtual void ShowOutline(bool value, Color color)
    {
        outline.enabled = value;
        if (outline.enabled)
            outline.OutlineColor = color;
    }

    public virtual void ShowError()
    {
        outline.enabled = true;
        StartCoroutine(ShowErrorCoro());
    }

    private IEnumerator ShowErrorCoro()
    {
        outline.OutlineMode = QuickOutline.Mode.OutlineAndSilhouette;
        int i = 0;
        while(i<3)
        {
            outline.OutlineColor = Color.red;
            yield return new WaitForSeconds(.3f);
            outline.OutlineColor = new Color(0,0,0,0);
            yield return new WaitForSeconds(.3f);
            i++;
        }
        outline.OutlineMode = QuickOutline.Mode.OutlineAll;
    }

    public virtual void InteractInvoke(bool value)
    {
        QuestManager.Instance.AddQuestCurrentAmount(goalType);
        ShowHintColor(false);
        afteInteract.Invoke();
    }
}
