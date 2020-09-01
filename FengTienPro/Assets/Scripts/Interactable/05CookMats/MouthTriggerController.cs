using UnityEngine;

public class MouthTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<IObjControllerBase>().goalType == Goal.Type.TasteFood)
        {
            other.gameObject.GetComponent<CookLadleController>().HaveRice(false);
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CookFood);
        }
        
    }
}
