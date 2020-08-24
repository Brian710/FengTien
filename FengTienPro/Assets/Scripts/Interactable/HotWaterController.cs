using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HotWaterController : InteracObjBase
{
    [SerializeField]
    private ParticleSystem partSys;
    [SerializeField]
    Animator glassAnim;
    

    public void ParticlePlay()
    {
        if (glassAnim.GetBool("full") || partSys.isPlaying)
            return;

     
        partSys.Play(true);
        glassAnim.SetBool("full", true);
        AudioManager.Instance.Play("Water_fall");
    }
}
