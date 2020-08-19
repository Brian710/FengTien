using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedCanV : OptionalSystemBase
{
    [SerializeField]
    CanvasGroup canvasGroup;
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
                    if (gameMode == MainMode.Train)
                    {
                        StartCoroutine(WrongAns(data.button.GetComponentInChildren<Text>()));
                        break;
                    }
                    else
                    {
                        IfRight = false;
                        break;
                    }
                }
            }
            i++;
        }

        int s = IfRight ? 0 : 1;

        QuestManager.Instance.AddQuestCurrentAmount(goalType);
        QuestManager.Instance.MinusQuestScore(s);
        CanvusOn(false);
    }

    public void CanvusOn(bool value)
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = value ? 1 : 0;
            canvasGroup.interactable = value;
        }
    }

    public override void QuizDatasInit()
    {
        if (quizPanel == null)
        {
            Debug.LogWarning("quizPanel is null");
            return;
        }

        quizDatas = new List<QuizData>();
        int i = 4;
        foreach (Button t in quizPanel.GetComponentsInChildren<Button>())
        {
            if (t.name.Contains("Qbtn"))
            {
                int index = 0;
                index = i;
                QuizData data = new QuizData();
                if (gameMode == MainMode.Exam)
                {
                    Debug.Log("Exam");
                    t.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
                }

                data.button = t;
                data.button.onClick.AddListener(() => base.QuizBtnOnclick(index));
                data.optIndex = -1;
                quizDatas.Add(data);
                i--;
            }
        }
    }
}
