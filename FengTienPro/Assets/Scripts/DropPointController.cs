using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointController : InteractableObjBase
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObjBase>().goalType == Goal.Type.WashObj)
        {
            base.InteractInvoke(true);
        }
    }
    public override void ShowInteractColor(bool value)
    {
        return;
    }
}
