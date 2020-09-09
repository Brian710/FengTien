using HTC.UnityPlugin.Vive;
using UnityEngine;

public class MedsCupController : IObjControllerBase , IGrabbable
{
    [SerializeField] private Animator Anim;
    [SerializeField] private BasicGrabbable _viveGrabFunc;
    [SerializeField] private HandAnim _handAnim;
    private int GrindNum;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;

    public new HandAnim handAnim => _handAnim;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.MixWater;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<BowlPillController>())
        {
            Anim.SetBool("Pour", true);
        }
    }
}
