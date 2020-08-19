using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SoupLadle_Soup" && GameController.Instance.currentGoal.type == Goal.Type.CookFood)
        {
            other.gameObject.GetComponent<CookLadleController>().HaveRice(false);
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CookFood);
        }
        
    }
}
