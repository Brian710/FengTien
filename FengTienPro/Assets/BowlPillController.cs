using UnityEngine;

public class BowlPillController : IObjControllerBase
{
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject Powder;
    [SerializeField] private Goal.Type type;
    [SerializeField] private Collider colli;
    private int GrindNum;
 

    public override void Awake()
    {
        base.Awake();
        goalType = type;
    }
    public override void Start()
    {
        if (Anim == null) Anim = ChildObj.GetComponent<Animator>();
        base.Start();
    }

    public override void SetChildObjActive(bool value)
    {
        if (value)
        {
            SetWaitingState();
        }
        else
        {
            ChildObj.SetActive(false);
            Powder.SetActive(false);
        }
    }
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        Anim.gameObject.SetActive(false);
        Powder.SetActive(false);
        colli.enabled = false;
    }

    protected override void SetCurrentState()
    {
        GrindNum = 0;
        Anim.gameObject.SetActive(true);
        Anim.SetInteger("CutNum", 0);
        Powder.SetActive(false);
        colli.enabled = true;
    }

    protected override void SetDoneState()
    {
        Anim.gameObject.SetActive(false);
        Powder.SetActive(true);
        colli.enabled = false;
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
}
