using System.Collections;
using UnityEngine;

public class TubeController : IObjControllerBase
{
    [SerializeField] private Animator Anim;
    public override void Start()
    {
        if (Anim == null) Anim = ChildObj.GetComponent<Animator>();
    }


}
