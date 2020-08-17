using HTC.UnityPlugin.ColliderEvent;
using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : InteractableObjBase
{
    [SerializeField]
    private Renderer lightMat;
    [SerializeField]
    private ParticleSystem PartSys;
    [SerializeField]
    private bool StepCompleted;


    private Coroutine _coroutine;
    private MaterialPropertyBlock _propBlock;
    private IWashable _IWashable;

    public override void Set()
    {
        base.Set();
        if (PartSys.isPlaying)
            PartSys.Stop(true);

        if (lightMat)
            SetLightColor(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (PartSys)
            PartSys.Play(true);
        
        SetLightColor(true);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _IWashable = other.gameObject.GetComponent<IWashable>();
        int washtime = _IWashable  == null ? 3 : _IWashable.WashTime();
            
        _coroutine = StartCoroutine(CountDownSecond(washtime));
     }

    public void OnTriggerExit(Collider other)
    {
        SetLightColor(false);

        if (PartSys)
            PartSys.Stop(true);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if(StepCompleted&& _IWashable != null)
            _IWashable.IsWashed(true);

        _IWashable = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (StepCompleted)
        {
            base.InteractInvoke(true);
            StepCompleted = false;
        }
    }

    private IEnumerator CountDownSecond(int max)
    {
        int i = 0;
        while (i <= max)
        {
            yield return 1f;
            i++;
        }
        StepCompleted = true;
    }

    private void SetLightColor(bool value)
    {
        lightMat.GetPropertyBlock(_propBlock);
        Color color = value ? Color.green : Color.red;
        _propBlock.SetColor("_EmissionColor", color);

        lightMat.SetPropertyBlock(_propBlock);
    }
}
