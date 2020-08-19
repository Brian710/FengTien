using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WashObj : InteractableObjBase,IWashable
{
    [SerializeField]
    private bool isWashed;
    [SerializeField]
    private int washTime;
    [SerializeField]
    private BasicGrabbable grabFunc;


    public string takeSound;
    public string dropSound;
    public string interactSound;
    private void Awake()
    {
        if (grabFunc == null)
            grabFunc = GetComponent<BasicGrabbable>();

        if (outline == null)
            outline = GetComponent<QuickOutline>();
    }
    public override void Set()
    {
        base.Set();
        

        ShowHintColor(true);
        grabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
        grabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;
    }

    public override void Remove()
    {
        base.Remove();
        grabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
        grabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
    }
    private void GrabFunc_afterGrabberGrabbed()
    {
        PlayerController.instance.RightHand.HandAnimChange(handAnim);
    }
    private void GrabFunc_beforeGrabberReleased()
    {
        PlayerController.instance.RightHand.HandAnimChange(HandAnim.Normal);
    }

    public void PlayTakeSound()
    {
        if (takeSound != "")
        {
            return;
        }
        AudioManager.Instance.Play(takeSound);
    }
    public void PlayInteractSound()
    {
        if (interactSound != "")
        {
            return;
        }
        AudioManager.Instance.Play(interactSound);
    }
    public void PlayDropSound()
    {
        if (dropSound != "")
        {
            return;
        }
        AudioManager.Instance.Play(dropSound);
    }

    public bool IsWashed(bool value)
    {
        return isWashed;
    }

    public int WashTime()
    {

        return washTime;
    }

    public override void InteractInvoke(bool value)
    {
    }
}
