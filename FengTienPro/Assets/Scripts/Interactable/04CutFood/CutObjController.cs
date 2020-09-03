using UnityEngine;

public class CutObjController : IObjControllerBase
{
    [SerializeField]    private Animator Anim;
    [SerializeField]    private GameObject Plate;
    [SerializeField]    private Goal.Type type;
    [SerializeField]    private Collider colli;
    private int CutNum;

    public override void Awake()
    {
        base.Awake();
        goalType = type;
    }
    public override void Start()
    {
        if (Anim == null)   Anim = ChildObj.GetComponent<Animator>();
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
            Plate.SetActive(false);
        }
    }
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        Anim.gameObject.SetActive(false);
        Plate.SetActive(false);
        colli.enabled = false;
    }

    protected override void SetCurrentState()
    {
        CutNum = 0;
        Anim.gameObject.SetActive(true);
        Anim.SetInteger("CutNum", 0);
        Plate.SetActive(false);
        colli.enabled = true;
    }

    protected override void SetDoneState()
    {
        Anim.gameObject.SetActive(false);
        Plate.SetActive(true);
        colli.enabled = false;
    }
    private bool KnifeIn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<KnifeController>())
        {
            KnifeIn = true;
            CutNum++;
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
            if(CutNum<4)
                Anim.SetInteger("CutNum", CutNum);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<KnifeController>())
            KnifeIn = false;
    }
}
