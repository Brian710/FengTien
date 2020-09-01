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
    private void OnTriggerEnter(Collider other)
    {
        CutNum++;
        QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CutFish);

        if (CutNum >= 4) CutNum = 0;
    }
}
