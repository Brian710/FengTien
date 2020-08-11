using HTC.UnityPlugin.ColliderEvent;
using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : InteractableObjBase
{
    [SerializeField]
    private Material lightMat;
    [SerializeField]
    private ParticleSystem PartSys;
    [SerializeField]
    private bool StepCompleted;

    private int i = 0;
    private void Start()
    {
        if (outline)
        {
            Debug.LogWarning("outline Start");
            outline.OutlineColor = hintColor;
            outline.enabled = false;
        }
        if (PartSys.isPlaying)
            PartSys.Stop(true);
        if (lightMat)
            lightMat.SetColor("_EmissionColor", Color.red);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (PartSys)
            PartSys.Play(true);
        StartCoroutine(CountDownSecond(3));
       
     }

    public void OnTriggerExit(Collider other)
    {
        if (lightMat)
            lightMat.SetColor("_EmissionColor", Color.green);
        if(PartSys)
            PartSys.Stop(true);
        StopAllCoroutines();
    }

    private void OnTriggerStay(Collider other)
    {
        if (StepCompleted)
        {
            base.InteractInvoke(true);
            i = 0;
            StepCompleted = false;
        }
    }

    private IEnumerator CountDownSecond(int max)
    {
        while (i <= max)
        {
            yield return 1f;
            i++;
        }
        StepCompleted = true;
    }
}
