using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class HandAnimManager : MonoBehaviour,IWashable
{
    [SerializeField]    private Animator animator;
    [SerializeField]    private HandAnim handAnim;
    [SerializeField]    private ParticleSystem Bubble;
    [SerializeField]    private int washTime;
    [SerializeField]    private bool isWashed;
    public GameObject Obj() => this.gameObject;
    public bool IsWashed() => isWashed;
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
        Bubble.Stop(true);
    }

    public void HandAnimChange(HandAnim value)
    {
        if (HandAnim == value && animator == null) { return; }

        HandAnim = value;
        animator.SetInteger("HandAnim", (int)HandAnim);
        PlayerController.Instance.AllRayActivity(value == HandAnim.Normal);
    }

    public void NormalAnimUpdate(float inputAxis)
    {
        if (handAnim != HandAnim.Normal) { return; }
        
        animator.SetFloat("Rotation", Mathf.Abs(inputAxis));
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            HandAnimChange(handAnim);
        }
    }
#endif
    public void SetWashed(bool value)
    {
        isWashed = value;
        if (isWashed)   QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.Tap);
    }

    public int WashTime()
    {
        return washTime;
    }

    public IEnumerator ShowBubble()
    {
        Bubble.Play(true);
        yield return new WaitForSeconds(2f);
        Bubble.Stop(true);

    }
}