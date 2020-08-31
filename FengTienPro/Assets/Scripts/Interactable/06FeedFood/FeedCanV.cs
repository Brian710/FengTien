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
        int quizIndex = Mathf.Abs(index - 4);
        if (!quizDatas[quizIndex].button.interactable)
        {
            quizDatas[quizIndex].optIndex = index;
            quizDatas[quizIndex].button.interactable = true;
            quizDatas[quizIndex].button.targetGraphic = options[index].GetComponent<Image>();
            quizDatas[quizIndex].button.GetComponentInChildren<Text>().text = options[index].GetComponentInChildren<Text>().text;
            quizDatas[quizIndex].button.GetComponentInChildren<Text>().color = Color.black;

            options[index].GetComponent<CanvasGroup>().alpha = 0;
            options[index].interactable = false;
        }
        else
        {
            if (mode == MainMode.Exam)
            {
                QuestManager.Instance.MinusQuestScore(2);
            }
            StartCoroutine(WrongAns(options[index].GetComponentInChildren<Text>()));
            return;
        }
    }

    public override void RandomPos()
    {

    }
}
