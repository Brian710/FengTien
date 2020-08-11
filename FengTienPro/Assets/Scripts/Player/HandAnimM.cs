using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MinYanGame.Core;
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
        animator.SetInteger("HandAnim", (int)HandAnim);
    }

    public void AnimUpdate(float inputAxis)
    {
        if (handAnim == HandAnim.normal)
        {
            animator.SetFloat("Rotation", Mathf.Abs(inputAxis));
        }
    }

    private void OnValidate()
    {
        //HandAnimChange(handAnim);
    }
}