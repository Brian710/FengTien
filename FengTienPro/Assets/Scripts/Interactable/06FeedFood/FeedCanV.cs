using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedCanV : OptionalSystemBase
{
    public CanvasGroup canvasGroup;
    public override void ConfirmBtn()
    {
        //bool IfRight = true;
        //int i = 0;
        //foreach (QuizData data in quizDatas)
        //{
        //    if (data.button.interactable)
        //    {
        //        if (data.optIndex != i)
        //        {
        //            StartCoroutine(WrongAns(data.button.GetComponentInChildren<Text>()));
        //             IfRight = false;
        //        }
        //        else
        //        {
        //            data.button.transform.SetAsFirstSibling();
        //        }
        //        break;
        //    }
        //    i++;
        //}
        //int s = IfRight ? 0 : 1;
        
        //QuestManager.Instance.MinusQuestScore(s);
        //CanvasOn(false);
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
            int index = i;
            QuizData newQuiz = new QuizData();
            if (mode == MainMode.Exam)
            {
                t.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
            }
            newQuiz.button = t;
            newQuiz.optIndex = index;
            newQuiz.button.onClick.AddListener(() => QuizBtnOnclick(index));
            quizDatas.Add(newQuiz);
            i--;
        }
    }

    public override void OptBtnOnclick(int index)
    {
        QuizData newQuizD = new QuizData();
        for (int i = 4; i >= 0; i--)
        {
            newQuizD = quizDatas[i];
            if (newQuizD.button.GetComponent<CanvasGroup>().alpha != 0)
            {
                if (newQuizD.optIndex == index)
                {
                    newQuizD.button.GetComponent<Image>().sprite = options[index].GetComponent<Image>().sprite;
                    newQuizD.button.GetComponentInChildren<Text>().text = hintText[index];
                    newQuizD.button.GetComponentInChildren<Text>().color = Color.black;

                    options[index].GetComponent<CanvasGroup>().alpha = 0;
                    options[index].interactable = false;
                    StartCoroutine(CanvasRoutineOff(i));
                }
                else
                {
                    QuestManager.Instance.MinusQuestScore(2);
                    StartCoroutine(WrongAns(options[index].GetComponentInChildren<Text>()));
                }
                return;
            }
        }
    }

    private IEnumerator CanvasRoutineOff(int quizIndex)
    {
        yield return new WaitForSeconds(1.5f);
        quizDatas[quizIndex].button.GetComponent<CanvasGroup>().alpha = 0;
        CanvasOn(false);
    }

    public override void RandomPos() { }
}
