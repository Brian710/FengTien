using HTC.UnityPlugin.Vive;
using UnityEngine;

public class BowlPillController : IObjControllerBase, IGrabbable
{
    [SerializeField] private Animator Anim;
    [SerializeField] private Collider colli;
    [SerializeField] private GameObject Pill;
    [SerializeField] private BasicGrabbable _viveGrabFunc;
    [SerializeField] private HandAnim _handAnim;
    private int GrindNum;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new HandAnim handAnim => _handAnim;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.GrindMeds;
    }
    public override void Start()
    {
        if (Anim == null) Anim = ChildObj.GetComponent<Animator>();
        viveGrabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        viveGrabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
        viveGrabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
    }

    public override void SetChildObjActive(bool value)
    {
        if (value)  SetWaitingState();
        else
        {
            ChildObj.SetActive(false);
        }
    }
    private bool PestleIn;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PestleController>())
        {
            PestleIn = true;
            GrindNum++;
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
            if (GrindNum < 3)
            {
                Anim.SetInteger("GrindNum", GrindNum);
                Debug.LogError("Grinding");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PestleController>())
            PestleIn = false;
    }
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        Anim.gameObject.SetActive(false);
        colli.enabled = false;
    }
    protected override void SetCurrentState()
    {
        GrindNum = 0;
        viveGrabFunc.enabled = false;
        Anim.gameObject.SetActive(true);
        Anim.SetInteger("GrindNum", 0);
        colli.enabled = true;
    }
    protected override void SetDoneState()
    {
        viveGrabFunc.enabled = true;
        colli.enabled = false;
        Pill.SetActive(false);
    }
}
