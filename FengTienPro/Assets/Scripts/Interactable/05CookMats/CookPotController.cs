﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPotController : IObjControllerBase
{
    [SerializeField]    private List<GameObject> CookMats;
    [SerializeField]    private GameObject Ladle;
    [SerializeField]    private Animator CookAnim;
    [SerializeField]    private ParticleSystem HeatFX;
    [SerializeField]    private ParticleSystem CookFX;
    [SerializeField]    private Animator CookUI;
    [SerializeField]    private ParticleSystem CookUIDone;
   

    private Coroutine _coutdownCoro;
    private int timer;

    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.CookFood;
    }

    protected override void SetWaitingState()
    {
        timer = 0;
        CookUIDone.Stop();
        Ladle.SetActive(false);

        foreach (GameObject obj in CookMats)
        {
            obj.SetActive(false);
        }
    }
    protected override void SetCurrentState()
    {
        Ladle.SetActive(true);
    }
    protected override void SetDoneState()
    {
        Ladle.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        InputMatObj FoodMat = other.GetComponentInParent<InputMatObj>();

        if (FoodMat == null || Mathf.Abs(FoodMat.transform.rotation.x) <= 0.2 || !FoodMat.IfHaveMats())
            return;

        QuestManager.Instance.AddQuestCurrentAmount(FoodMat.goalType);
        switch (FoodMat.goalType)
        {
            case Goal.Type.InputRice:
                CookMats[0].SetActive(true);
                break;
            case Goal.Type.InputWater:
                CookMats[1].SetActive(true);
                break;
            case Goal.Type.InputFish:
                CookMats[2].SetActive(true);
                break;
            case Goal.Type.InputVeg:
                CookMats[3].SetActive(true);
                break;
        }
    }
    private void CookAnimOn(bool value)
    {
        CookAnim.SetBool("On", value);
        if (value)
        {
            CookFX.Play(true);
            HeatFX.Play(true);
        }
        else
        {
            CookFX.Stop(true);
            HeatFX.Stop(true);
        }
    }
    public void CookCoutdown(bool value)
    {
        if (value)
        {
            if (_coutdownCoro != null)
                StopCoroutine(_coutdownCoro);
            CookAnimOn(true);
            _coutdownCoro = StartCoroutine(CoundownTimer(10, 15));
        }
        else
        {
            if (_coutdownCoro == null)
                return;

            StopCoroutine(_coutdownCoro);
            CookAnimOn(false);

            if (10 <= timer && timer <= 14)
            {
                CookUIDone.Play();
                QuestManager.Instance.AddQuestCurrentAmount(goalType);
            }
            else
            {
                QuestManager.Instance.MinusQuestScore(1);
                QuestManager.Instance.ReopenQuestGiver();
            }
        }
    }

    IEnumerator CoundownTimer(int cooked, int overcook)
    {
        while (timer < overcook)
        {
            yield return new WaitForSeconds(1f);
            CookUI.SetFloat("CookTime", timer / cooked);
            timer++;
        }
        CookCoutdown(false);
    }
}
