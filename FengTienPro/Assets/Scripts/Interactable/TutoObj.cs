using UnityEngine;

public class TutoObj : InteractableObjBase
{
    [SerializeField]
    private HandAnim handAnim;

    public string takeSound;
    public string dropSound;
    public string interactSound;

    public override void Set()
    {
        base.Set();
        ShowHintColor(true);
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

    public override void InteractInvoke(bool value)
    {
    }
}
