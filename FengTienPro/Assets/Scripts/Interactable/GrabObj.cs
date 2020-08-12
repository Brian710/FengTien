using UnityEngine;

public class GrabObj : InteractableObjBase
{
    [SerializeField]
    private GrabObject grabData;
    [SerializeField]
    private HandAnim handAnim;

    public string takeSound;
    public string dropSound;
    public string interactSound;

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
}
