using System.Collections;
using UnityEngine;

public class CutObjController : IObjControllerBase
{
    public Animator Anim;
    [SerializeField]
    private GameObject Plate;
    public override void Start()
    {
        base.Start();
        if (Anim == null)
            Debug.LogWarning("cut trans not set!");
    }
    protected override void SetWaitingState()
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

    public override void InteractInvoke(bool value)
    {
        DelaySetActive(value);
    }

    IEnumerator DelaySetActive(bool value)
    {
        yield return new WaitForSeconds(1.5f);
        //afteInteract.Invoke();
    }
}
