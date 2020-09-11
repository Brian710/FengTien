using HTC.UnityPlugin.Vive;
using UnityEngine;

public class FeedWaterController : IObjControllerBase, IGrabbable
{
    [SerializeField]    private GameObject On;
    [SerializeField]    private Goal.Type type;
    [SerializeField]    private GameObject Spoon;
    [SerializeField]    private GameObject Bowl;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;

    public GlassController glass;
    public override void Awake()
    {
        base.Awake();
        goalType = type;
    }

    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        glass.doFull(false);
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        HaveMats(true);
        Spoon.SetActive(false);
        Bowl.SetActive(false);
        //PlayerController.Instance.LeftHand.HandAnimChange(HandAnim.Normal);
        //PlayerController.Instance.RightHand.HandAnimChange(HandAnim.Normal);
        glass.doFull(true);
        viveGrabFunc.enabled = true;
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        HaveMats(false);
        glass.doFull(false);
        viveGrabFunc.enabled = false;
        base.SetDoneState();
    }

    public void HaveMats(bool value)
    {
        if (On)
            On.SetActive(value);
    }

    public bool IfHaveMats()
    {
        return On.activeSelf;
    }
}
