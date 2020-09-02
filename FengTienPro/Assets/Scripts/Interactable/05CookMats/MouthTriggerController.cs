using UnityEngine;

public class MouthTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CookLadleController cookLadle = other.GetComponentInParent<CookLadleController>();

        if (cookLadle)
        {
            if (cookLadle.IfHaveMat())
            {
                cookLadle.HaveRice(false);
                QuestManager.Instance.AddQuestCurrentAmount(cookLadle.goalType);
            }
        }
    }
}
