using UnityEngine;

public class BowlPillController : IObjControllerBase
{
    [SerializeField] private Animator Anim;
    [SerializeField] private ClicktoInteract ClickInteract;
    [SerializeField] private Goal.Type type;
    [SerializeField] private Collider TriggerColli;
    private int GrindNum;
    private bool PestleIn;

    private Vector3 AlignPos;
    private Quaternion AlignRot;

    public override void Awake()
    {
        base.Awake();
        goalType = type;
        AlignPos = new Vector3(-0.016f, 0.0024f, -0.0126f);
        AlignRot = Quaternion.Euler(0, 0, 0);
    }
    public override void Start()
    {
        if (Anim == null) Anim = ChildObj.GetComponent<Animator>();
        base.Start();
    }
    
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        TriggerColli.enabled = false;
    }

    protected override void SetCurrentState()
    {
        GrindNum = 0;
        Anim.SetInteger("GrindNum", GrindNum);
        TriggerColli.enabled = true;
    }

    protected override void SetDoneState()
    {
        Anim.gameObject.SetActive(false);
        TriggerColli.enabled = false;
    }
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
}
