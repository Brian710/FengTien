﻿using UnityEngine;

public class TutoCheckPoint : CheckPointBase
{
    public override void Start()
    {
        ShowParticle(true);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InteractableObjBase>().goalType == Goal.Type.Tuto)
        {
            onTriggerEnter.Invoke();
        }
    }


    public override void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
            other.attachedRigidbody.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }
}
