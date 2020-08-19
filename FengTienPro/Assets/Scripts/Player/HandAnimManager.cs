using UnityEngine;
using UnityEngine.Events;
public class HandAnimManager : MonoBehaviour,IWashable
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private HandAnim handAnim;
    [SerializeField]
    private int washTime;
    [SerializeField]
    private bool isWashed;

    [SerializeField]
    private bool isFirstTime;

    public UnityEvent afteInteract;

    public void NoWashed() => isWashed = false;
    public HandAnim HandAnim
    {
        get { return handAnim; }
        set { handAnim = value; }
    }

    private void Start()
    {
        handAnim = HandAnim.Normal;
        washTime = 3;
        isWashed = false;
    }

    public void HandAnimChange(HandAnim value)
    {
        Debug.LogWarning((int)handAnim);
        if (HandAnim != value)
        {
            HandAnim = value;
        }
        if(animator)
            animator.SetInteger("HandAnim", (int)HandAnim);
    }

    public void NormalAnimUpdate(float inputAxis)
    {
        if (handAnim == HandAnim.Normal)
        {
            animator.SetFloat("Rotation", Mathf.Abs(inputAxis));
        }
    }
    
    private void OnValidate()
    {
        HandAnimChange(handAnim);
    }

    public bool IsWashed(bool value)
    {
        if (isWashed == value)
            return isWashed;

        isWashed = value;
        if (isWashed)
        {
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.Tap);
        }
        return isWashed;
    }

    public int WashTime()
    {
        return washTime;
    }
}