﻿using UnityEngine;
using UnityEngine.Events;

public class OldManMouthTrigger : MonoBehaviour
{
    private int BiteNum;
    [SerializeField]    private Animator EatAnim;
    [SerializeField]    private FeedCanV FeedCanV;
    [SerializeField] private GameObject MouseTrigger;

    private void Start()
    {
        MouseTrigger.SetActive(false);
    }
    private void OnEnable()
    {
        Set();
    }
    public void Set()
    {
        BiteNum = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SpoonController>() && FeedCanV.canvasGroup.alpha != 1)
        {
            if (other.gameObject.GetComponent<SpoonController>().IfHaveMat() && FeedCanV.canvasGroup.alpha != 1)
            {
                //PlayerController.Instance.EnableRightRay = false;
                EatAnim.SetTrigger("EatState");
                other.gameObject.GetComponent<SpoonController>().GetMat(false);
                QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.FeedFood);
                BiteNum++;

                if (BiteNum <= 4)
                    FeedCanV.CanvasOn(true);
            }
        }
        else if (other.gameObject.GetComponent<GlassController>())
        {
            if (other.gameObject.GetComponent<GlassController>().isFull())
            {
                QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.FeedWater);
                other.gameObject.GetComponent<GlassController>().doFull(false);
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.GetComponent<SpoonController>() && FeedCanV.canvasGroup.alpha != 1)
    //    {
    //        if (other.gameObject.GetComponent<SpoonController>().IfHaveMat() && FeedCanV.canvasGroup.alpha != 1)
    //        {
    //            PlayerController.Instance.EnableRightRay = true;
    //        }
    //    }
    //}
}
