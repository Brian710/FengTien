using System.Collections.Generic;
using UnityEngine;

public class ShowHandWash : MonoBehaviour
{
    public int AnimInt;
    public  Animator HandWashing;
    private void Start()
    {
        //AnimInt = -2;
        HandWashing.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ShowAnimation(true, AnimInt);
    }

    private void OnDisable()
    {
        ShowAnimation(false, AnimInt);
    }

    public void ShowAnimation(bool value,int AnimInt)
    {
        if (GameController.Instance.mode == MainMode.Exam || HandWashing == null)
        {
            return;
        }

        HandWashing.gameObject.SetActive(value);
        if (value)
        {
            HandWashing.SetInteger("AnyState", AnimInt);
        }
    }

    public void ShowAnimation(bool value)
    {
        if (GameController.Instance.mode == MainMode.Exam)
        {
            return;
        }

        if (value)
        {
            HandWashing.gameObject.SetActive(value);
            HandWashing.SetBool("AutoShow", value);
            HandWashing.SetInteger("AnyState", -2);
        }
        else
        {
            HandWashing.SetBool("AutoShow", value);
            HandWashing.SetInteger("AnyState", -1);
            HandWashing.gameObject.SetActive(value);
        }

    }

}
