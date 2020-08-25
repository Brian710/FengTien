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
    [SerializeField]
    private GameObject PlateFish;
    [SerializeField]
    private GameObject PlateVeg;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Knife" && Fish.gameObject.activeSelf)
        {
            CutNum++;
            Fish.Anim.SetInteger("CutNum", CutNum);
            QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CutFish);
            if (CutNum >= 4)
                Fish.InteractInvoke(true);
        }
    }
}
