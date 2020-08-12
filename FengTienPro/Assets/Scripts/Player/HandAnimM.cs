using UnityEngine;
public class HandAnimM : MonoBehaviour
{
    [SerializeField]
    private Transform traget;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private HandAnim handAnim;

    private Transform tracker;
    public HandAnim HandAnim
    {
        get { return handAnim; }
        set { handAnim = value; }
    }

    private void GetDevice()
    {
        tracker = transform.parent;
    }

    private void Start()
    {
        GetDevice();
        handAnim = HandAnim.normal;
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
        if (handAnim == HandAnim.normal)
        {
            animator.SetFloat("Rotation", Mathf.Abs(inputAxis));
        }
    }
    
    private void OnValidate()
    {
        HandAnimChange(handAnim);
    }
}