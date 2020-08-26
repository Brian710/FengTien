using System.Collections;
using UnityEngine;

public class CutObjController : IObjControllerBase
{
    public Animator Anim;
    [SerializeField]
    private GameObject Plate;
    [SerializeField]
    private Goal.Type type;

    public override void Awake()
    {
        base.Awake();
        goalType = type;
    }
    public override void Start()
    {
        base.Start();
        if (Anim == null)
            Debug.LogWarning("cut trans not set!");
    }
    
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        Plate.SetActive(false);
    }

    protected override void SetCurrentState()
    {
        Anim.gameObject.SetActive(true);
        Anim.SetInteger("CutNum", 0);
        Plate.SetActive(false);
    }

    protected override void SetDoneState()
    {
        Anim.gameObject.SetActive(false);
        Plate.SetActive(true);
    }
}
