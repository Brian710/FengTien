using UnityEngine;

public class WashObj : InteractableObjBase,IWashable
{
    [SerializeField]
    private bool isWashed;
    [SerializeField]
    private int washTime;
   


    public string takeSound;
    public string dropSound;
    public string interactSound;
    private void Awake()
    {
        if (outline == null)
            outline = GetComponent<QuickOutline>();
    }
    public override void Set()
    {
        base.Set();

        ShowHintColor(true);
        
    }

    public override void Remove()
    {
        base.Remove();
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
