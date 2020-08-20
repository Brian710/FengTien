﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPotController : InteractableObjBase
{
    [SerializeField]
    private List<GameObject> gameObjects;

    [SerializeField]
    private List<InteractableObjBase> FoodMats;

    [SerializeField]
    private CookLadleController Ladle;

    [SerializeField]
    private Animator CookAnim;

    [SerializeField]
    private Animator CookUI;

    [SerializeField]
    private ParticleSystem cookdone;

    private Coroutine _coutdownCoro;
    private int timer;

    List<Vector3> FoodMatPos = new List<Vector3>();
    List<Quaternion> FoodMatRot = new List<Quaternion>();

    public override void Set()
    {
        timer = 0;
        cookdone.Stop();
        FoodMatPos.Clear();
        FoodMatRot.Clear();
        base.Set();
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
        foreach (InteractableObjBase trans in FoodMats)
        {
            trans.Set();
            FoodMatPos.Add(trans.transform.position);
            FoodMatRot.Add(trans.transform.rotation);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Mathf.Abs(other.transform.rotation.x) <= 0.2)
        {
            return;
        }

        if (other.gameObject == FoodMats[0].gameObject)
        {
            gameObjects[0].SetActive(true);
            other.gameObject.transform.position = FoodMatPos[0];
            other.gameObject.transform.rotation = FoodMatRot[0];
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
        }
        else if (gameObjects[0].activeSelf && other.gameObject == FoodMats[1].gameObject)
        {
            gameObjects[1].SetActive(true);
            other.gameObject.transform.position = FoodMatPos[1];
            other.gameObject.transform.rotation = FoodMatRot[1];
            QuestManager.Instance.AddQuestCurrentAmount(goalType);

        }
        else if (gameObjects[1].activeSelf && other.gameObject == FoodMats[2].gameObject)
        {
            gameObjects[2].SetActive(true);
            other.gameObject.transform.position = FoodMatPos[2];
            other.gameObject.transform.rotation = FoodMatRot[2];
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
        }
        else if (gameObjects[2].activeSelf && other.gameObject == FoodMats[3].gameObject)
        {
            gameObjects[3].SetActive(true);
            other.gameObject.transform.position = FoodMatPos[3];
            other.gameObject.transform.rotation = FoodMatRot[3];
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
        }
        else if (gameObjects[3].activeSelf && other.gameObject == FoodMats[4].gameObject)
        {
            InteractInvoke(true);
            other.gameObject.transform.position = FoodMatPos[4];
            other.gameObject.transform.rotation = FoodMatRot[4];
            goalType = Goal.Type.CookFood;
        }
        else
        {
            int i = 0;
            foreach (InteractableObjBase obj in FoodMats)
            {
                if (other.gameObject == obj.gameObject)
                {
                    if (GameController.Instance.mode == MainMode.Exam)
                        QuestManager.Instance.MinusQuestScore(1);

                    obj.ShowError();
                    other.gameObject.transform.position = FoodMatPos[i];
                    other.gameObject.transform.rotation = FoodMatRot[i];
                }
                i++;
            }
        }
    }

    public void OnCook(bool value)
    {
        if (CookAnim)
        {
            CookAnim.SetBool("On", value);
        }
    }

    public void CookCoutdown(bool value)
    {
        if (value)
        {
            OnCook(true);
            if (_coutdownCoro != null)
                StopCoroutine(_coutdownCoro);

            _coutdownCoro = StartCoroutine(CoundownTimer(10, 15));
        }
        else
        {
            if (_coutdownCoro == null)
                return;

            if (10 <= timer && timer <= 15)
            {
                cookdone.Play();
                QuestManager.Instance.AddQuestCurrentAmount(goalType);
            }
            
            OnCook(false);

            if (_coutdownCoro != null)
                StopCoroutine(_coutdownCoro);
            if(timer > 15)
                QuestManager.Instance.ReopenQuestGiver();
        }
    }

    IEnumerator CoundownTimer(int cooked, int overcook)
    {
        while (timer <= cooked)
        {
            yield return new WaitForSeconds(1f);
            CookUI.SetFloat("CookTime", timer / cooked);
            timer++;
        }
        Ladle.HaveRice(true);
        while (timer <= overcook)
        {
            yield return new WaitForSeconds(1f);
            CookUI.SetFloat("CookTime", timer / cooked);
            timer++;
        }
        CookCoutdown(false);
    }
}
