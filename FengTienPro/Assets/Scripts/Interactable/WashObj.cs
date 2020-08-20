using UnityEngine;

public class WashObj : InteractableObjBase, IWashable
{
    [SerializeField]
    private bool isWashed;
    [SerializeField]
    private int washTime;

    public string takeSound;
    public string dropSound;
    public string interactSound;

    public override void Awake()
    {
        base.Awake();
        if (outline == null)
            outline = GetComponent<QuickOutline>();

        goalType = Goal.Type.WashObj;
    }
    public override void Set() => ShowHintColor(true);

    public override void Remove()
    {
        base.Remove();
    }

    public override void GrabFunc_afterGrabberGrabbed()
    {
        base.GrabFunc_afterGrabberGrabbed();
        PlayTakeSound();
    }

    public void SetGrabble(bool value)
    {
        if (grabFunc == null)
            return;

        grabFunc.enabled = value;
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
        if (isWashed == value)
            return isWashed;

        isWashed = value;
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
