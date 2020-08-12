﻿using HTC.UnityPlugin.Pointer3D;
using HTC.UnityPlugin.Vive;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleportManager : Teleportable
{
    [SerializeField]
    protected ParticleSystem defaultFX;
    [SerializeField]
    protected ParticleSystem FX_On;

    [SerializeField]
    protected bool isActive;

    public virtual void Start()
    {
        target = PlayerController.instance.Target;
        pivot = PlayerController.instance.Cam;
        defaultFX.Play(false);
        isActive = false;
    }


    protected override void OnPointerTeleport(PointerEventData eventData)
    {
        if(isActive)
            base.OnPointerTeleport(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!isActive)
        {
            Debug.LogWarning("Teleportable is not active");
            return;
        }

        base.OnPointerDown(eventData);
        if (FX_On)
            FX_On.Play(true);
    }

    public override void OnPointer3DPressExit(Pointer3DEventData eventData)
    {
        if (!isActive)
        {
            Debug.LogWarning("Teleportable is not active");
            return;
        }

        base.OnPointer3DPressExit(eventData);
        if (FX_On)
            FX_On.Stop(true);
    }
    public virtual  void OnEnable()
    {
        OnAfterTeleport += TP_OnAfterTeleport;
    }
    public virtual void OnDisable()
    {
        OnAfterTeleport -= TP_OnAfterTeleport;
    }

    public virtual void TP_OnAfterTeleport(Teleportable src, RaycastResult hitResult, float delay)
    {
        //default invoke
    }

    public void ShowTeleport(bool value)
    {
        if (defaultFX)
        {
            if (value)
                defaultFX.Play(true);
            else
                defaultFX.Stop(true);
        }

        isActive = value;
    }
}
