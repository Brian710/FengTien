using System.Collections;
using UnityEngine;

public class CutObjController : IObjControllerBase
{
    [SerializeField]    private Animator Anim;
    [SerializeField]    private GameObject Plate;
    [SerializeField]    private Goal.Type type;
    [SerializeField]    private Collider colli;
    [SerializeField]    private Collider Knife;
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
    protected override void SetWaitingState()
    {
        //Debug.LogError($"{goalType} Wait");
        base.SetWaitingState();
        Anim.gameObject.SetActive(false);
        Plate.SetActive(false);
        colli.enabled = false;
    }

    protected override void SetCurrentState()
    {
        //Debug.LogError($"{goalType} Current");
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
        if (other==Knife && !KnifeIn)
        {
            KnifeIn = true;
            CutNum++;
            if (CutNum <= 4)
            { 
                Anim.SetInteger("CutNum", CutNum);
                //Debug.LogError($"{name}_ CutNum: {CutNum}");
            }
            StartCoroutine(DelayAdd());
        }
    }


    private IEnumerator DelayAdd()
    {
        yield return new WaitForSeconds(1.5f);
        QuestManager.Instance.AddQuestCurrentAmount(goalType);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<KnifeController>())
            StartCoroutine(DelayKnife(false));
    }

    private IEnumerator DelayKnife(bool value)
    {
        yield return new WaitForSeconds(1f);
        KnifeIn = value;
    }
}
