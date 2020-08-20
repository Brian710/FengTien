using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OldManMouthTrigger : MonoBehaviour
{
    private int BiteNum;
    private bool GlassIn;
    [SerializeField]
    private Animator EatAnim;

    public FeedCanV FeedCanV;

    public UnityEvent finishedFeed;
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
                EatAnim.SetTrigger("EatState");
                other.gameObject.GetComponent<SpoonController>().HaveMat(false);
                QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.FeedFood);
                BiteNum++;

                if (BiteNum <= 4)
                    FeedCanV.CanvasOn(true);
                else
                {
                    finishedFeed.Invoke();
                }
            }
        }
        else if (other.gameObject.GetComponent<GlassController>())
        {
            if(other.gameObject.GetComponent<GlassController>().isFull())
                GlassIn = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GlassIn && other.gameObject.GetComponent<GlassController>().isPour())
        {
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.DrinkWater);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<GlassController>())
            GlassIn = false;
    }
}
