using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutObjController : InteractableObjBase
{
    private int CutNum = 0;
    [SerializeField]
    private Animator Trans;

    public override void Set()
    {
        CutNum = 0;
        ShowHintColor(true);
        Trans.SetInteger("CutNum", CutNum);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Knife")
        { 
            CutNum++;
            Trans.SetInteger("CutNum", CutNum);
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
        }

        if (CutNum >= 4)
            InteractInvoke(true);
    }

    public override void InteractInvoke(bool value)
    {
        DelaySetActive(false);
    }

    IEnumerator DelaySetActive(bool value)
    {
        yield return new WaitForSeconds(1.5f);
        ShowHintColor(false);
        afteInteract.Invoke();
        gameObject.SetActive(false);
    }
}
