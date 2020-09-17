using HTC.UnityPlugin.Vive;
using System.Collections;
using UnityEngine;

public class BowlPillController : IObjControllerBase, IGrabbable
{
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject Pill;
    private int GrindNum;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.GrindMeds;
    }
    public override void Start()
    {
        if (Anim == null) Anim = ChildObj.GetComponent<Animator>();
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
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
            if (GrindNum <= 3)
            {
                Anim.SetInteger("GrindNum", GrindNum);
                Debug.LogError($"{name}_ GrindNum: {GrindNum}");
            }
            StartCoroutine(DelayAdd());
        }
        if (other.GetComponentInParent<MedsCupController>())
        {
            ChildObj.transform.position = new Vector3(0, 0, 0);
            ChildObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            viveGrabFunc.enabled = false;
        }
    }
    private IEnumerator DelayAdd()
    {
        yield return new WaitForSeconds(1.7f);
        QuestManager.Instance.AddQuestCurrentAmount(goalType);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PestleController>())
            StartCoroutine(DelayKnife(false));
    }
    private IEnumerator DelayKnife(bool value)
    {
        yield return new WaitForSeconds(1f);
        PestleIn = value;
    }
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        Anim.gameObject.SetActive(false);
    }
    protected override void SetCurrentState()
    {
        GrindNum = 0;
        viveGrabFunc.enabled = false;
        Anim.gameObject.SetActive(true);
        Anim.SetInteger("GrindNum", 0);
        hover.enabled = true;
        hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
    }
    protected override void SetDoneState()
    {
        viveGrabFunc.enabled = true;
        hover.enabled = true;
        Pill.SetActive(false);
    }
}
