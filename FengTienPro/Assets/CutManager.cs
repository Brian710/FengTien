using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutManager : MonoBehaviour
{
    int CutNum;
    [SerializeField]
    private CutObjController Fish;
    [SerializeField]
    private CutObjController Veg;
    private void OnEnable()
    {
        CutNum = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Knife")
        {
            if (Fish.gameObject.activeSelf)
            {
                CutNum++;
                Fish.Anim.SetInteger("CutNum", CutNum);
                QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CutFish);
                
            }
            else if (Veg.gameObject.activeSelf)
            {
                CutNum++;
                Veg.Anim.SetInteger("CutNum", CutNum);
                QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CutVeg);
            }

            if (CutNum >= 4) CutNum = 0;
        }
    }
}
