using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedCanV : OptionalSystemBase
{
    public CanvasGroup canvasGroup;
    public override void ConfirmBtn()
    {
        bool IfRight = true;
        int i = 0;
        foreach (QuizData data in quizDatas)
        {
            if (data.button.interactable)
            {
                if (data.optIndex != i)
                {
                    if (mode == MainMode.Train)
                    {
                        StartCoroutine(WrongAns(data.button.GetComponentInChildren<Text>()));
                    }
                    else
                    {
                        IfRight = false;
                    }
                }
                else
                {
                    data.button.transform.SetAsFirstSibling();
                }
                break;
            }
            i++;
        }
        int s = IfRight ? 0 : 1;
        
        QuestManager.Instance.MinusQuestScore(s);
        CanvasOn(false);
    }

    public void CanvasOn(bool value)
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = value ? 1 : 0;
            canvasGroup.interactable = value;
        }
    }

    public override void QuizDatasInit()
    {
        quizDatas = new List<QuizData>();
        int i = 4;
        foreach (Button t in quizPanel.GetComponentsInChildren<Button>())
        {
            int index = 0;
            index = i;
            QuizData data = new QuizData();
            if (mode == MainMode.Exam)
            {
                Debug.Log("Exam");
                t.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
            }
            data.button = t;
            data.button.onClick.AddListener(() => QuizBtnOnclick(index));
            data.optIndex = index;
            quizDatas.Add(data);
            i--;
        }
    }

    public override void OptBtnOnclick(int index)
    {
        if (!quizDatas[Mathf.Abs(index - 4)].button.interactable)
        {
            options[index].GetComponent<CanvasGroup>().alpha = 0;
            options[index].interactable = false;
        }
        for (int i = 4; i >= 0; i--)
        {
            quizData = quizDatas[i];
            if (!quizData.button.interactable)
            {
                quizData.optIndex = index;
                quizData.button.interactable = true;
                quizData.button.targetGraphic = options[index].GetComponent<Image>();
                quizData.button.GetComponentInChildren<Text>().text = options[index].GetComponentInChildren<Text>().text;
                quizData.button.GetComponentInChildren<Text>().color = Color.black;
                return;
            }
        }
        
    }

    public override void RandomPos()
    {

    }
}
