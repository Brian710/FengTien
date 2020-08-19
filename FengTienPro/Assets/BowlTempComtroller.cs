using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlTempComtroller : MonoBehaviour
{
    [SerializeField]
    GameObject spoon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Handtemper")
        {
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.TakeBowl);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            spoon.GetComponent<SpoonController>().ShowHintColor(true);
        }
    }
}
