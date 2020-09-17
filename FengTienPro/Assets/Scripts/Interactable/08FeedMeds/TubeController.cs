using HTC.UnityPlugin.ColliderEvent;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TubeController : ClicktoInteract
{
    [SerializeField] private HandAnim _handAnim;
    [SerializeField] private Animator Anim;
    [SerializeField] public GameObject TubeTips;
    [SerializeField] private GameObject SuctionBody;
    [SerializeField] private Collider SyringeCollider;
    public bool SuctionUpAvailable, SuctionDownAvailable, SuctionDone;
    private bool MedsUpAvailable, MedsDownAvailable_1, MedsDownAvailable_2, MedsDone;
    public HandAnim handAnim => _handAnim;
    public  void Start()
    {
        SuctionDownAvailable = false;
        SuctionDone = false;

        MedsUpAvailable = false;
        MedsDownAvailable_1 = false;
        MedsDownAvailable_2 = false;
        MedsDone = false;
        TubeTips.transform.localPosition = new Vector3(0, 0.0164f, 0);
    }
    public override void OnColliderEventClick(ColliderButtonEventData eventData)
    {
        if (pressingEvents.Contains(eventData) && pressingEvents.Count == 1)
        {
            if (QuestManager.Instance.GetQuestGoalByType(Goal.Type.CheckNasogastricTube).state == Goal.State.CURRENT)
            {
                if (SuctionUpAvailable)
                {
                    Anim.SetInteger("FoldState", 1);
                    Debug.LogError("抽取胃液");
                    SuctionUpAvailable = false;
                    SuctionDownAvailable = true;
                }
                else if (SuctionDownAvailable)
                {
                    Anim.SetInteger("FoldState", 2);
                    Debug.LogError("擠回胃液");
                    Debug.LogError("確認鼻胃管在胃中");
                    QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CheckNasogastricTube);
                    TubeTips.transform.localPosition = new Vector3(0, 0.07934f, 0);
                    Anim.SetInteger("FoldState", 5);
                    SuctionDownAvailable = false;
                    SuctionDone = true;
                }
            }
            else if (QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).state == Goal.State.CURRENT)
            {
                if (MedsUpAvailable)
                {
                    Anim.SetInteger("FoldState", 4);
                    Debug.LogError("放開管,讓藥進胃");
                    MedsUpAvailable = false;
                    MedsDownAvailable_1 = true;
                }
                else if (MedsDownAvailable_2)
                {
                    Anim.SetInteger("FoldState", 4);
                    QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.FeedMeds);
                    Debug.LogError("放開管,讓水進胃");
                    MedsDownAvailable_2 = false;
                    MedsDone = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == SyringeCollider && !SuctionUpAvailable)
        {
            other.gameObject.SetActive(false);
            TubeTips.transform.localPosition = new Vector3(0, 0.0863f, 0);
            SuctionBody.SetActive(true);
            SuctionUpAvailable = true;
            Debug.LogError("鼻胃管已連接, 可以準備開始抽吸");
        }
        if(other.GetComponentInParent<MedsCupController>() && SuctionDone)
        {
            Anim.SetInteger("FoldState", 3);
            Debug.LogError("將藥倒進管內");
            MedsUpAvailable = true;
        }
        else if(other.GetComponentInParent<MedsWaterController>() && MedsDownAvailable_1)
        {
            Anim.SetInteger("FoldState", 3);
            Debug.LogError("將水倒進管內");
            MedsDownAvailable_1 = false;
            MedsDownAvailable_2 = true;
        }
    }
}
