using HTC.UnityPlugin.ColliderEvent;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : ClicktoInteract
{
    [SerializeField] private Animator Anim;
    [SerializeField] private Goal.Type goalType;
    [SerializeField] private GameObject TubeTips;
    [SerializeField] private GameObject Syringe;
    [SerializeField] private GameObject Suction_Core;
    private bool SuctionUpAvailable, SuctionDownAvailable, SuctionDone;
    private bool MedsUpAvailable, MedsDownAvailable_1, MedsDownAvailable_2, MedsDone;
    public  void Start()
    {
        SuctionUpAvailable = false;
        SuctionDownAvailable = false;
        SuctionDone = false;

        MedsUpAvailable = false;
        MedsDownAvailable_1 = false;
        MedsDownAvailable_2 = false;
        MedsDone = false;

        if (Anim == null) Anim = Syringe.GetComponent<Animator>();
        Syringe.SetActive(false);
        Suction_Core.SetActive(true);
        TubeTips.transform.localPosition = new Vector3(0, 0.0164f, 0);
    }
    public override void OnColliderEventClick(ColliderButtonEventData eventData)
    {
        if (pressingEvents.Contains(eventData) && pressingEvents.Count == 1)
        {
           if(QuestManager.Instance.GetQuestGoalByType(Goal.Type.CheckNasogastricTube).state == Goal.State.CURRENT)
           {
                if(!SuctionUpAvailable && !SuctionDownAvailable)
                {
                    Syringe.SetActive(true);
                    TubeTips.transform.localPosition = new Vector3(0, 0.0863f, 0);
                    Anim.SetInteger("FoldState", 0);
                    SuctionUpAvailable = true;
                }               
                else if (SuctionUpAvailable)
                {
                    Anim.SetInteger("FoldState", 1);
                    Anim.SetFloat("Pull", Time.deltaTime * 0.1f);
                    Debug.LogError("抽取胃液");
                    SuctionUpAvailable = false;
                    SuctionDownAvailable = true;
                }
                else if (SuctionDownAvailable)
                {
                    Anim.SetInteger("FoldState", 1);
                    Anim.SetFloat("Pull", 1 - Time.deltaTime * 0.1f);
                    QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CheckNasogastricTube);
                    Debug.LogError("擠回胃液");
                    Debug.LogError("確認鼻胃管在胃中");
                    TubeTips.transform.localPosition = new Vector3(0, 0.07934f, 0);
                    Suction_Core.SetActive(false);
                    SuctionDownAvailable = false;
                    SuctionDone = true;
                }
            }
            else if (QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).state == Goal.State.CURRENT)
            {
                if (!MedsUpAvailable && !MedsDone)
                {
                    Anim.SetInteger("FoldState", 3);
                    Anim.SetFloat("Pull", Time.deltaTime * 0.1f);
                    Debug.LogError("將藥倒進管內");
                    MedsUpAvailable = true;
                }
                else if (MedsUpAvailable)
                {

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
