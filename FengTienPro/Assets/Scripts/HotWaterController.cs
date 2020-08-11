using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HotWaterController : InteractableObjBase
{
    [SerializeField]
    private ParticleSystem partSys;

    public void ParticlePlay()
    {
        if (partSys.isStopped)
            partSys.Play(true);
        else
            partSys.Stop(true);
    }
}
