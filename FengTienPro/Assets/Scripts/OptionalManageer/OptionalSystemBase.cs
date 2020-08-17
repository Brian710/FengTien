using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MinYanGame.Core;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class OptionalSystemBase : MonoBehaviour
{
    [SerializeField]
    private Goal.Type goalType;
    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private GameObject quizPanel;
    [SerializeField]
    private Button confirmBtn;

    private List<Button> options;
    private List<QuizData> quizDatas;
    private List<string> hintText;

    private MainMode gameMode;

    private void OnEnable()
    {
        CheckMode();
        OptionsInit();
        QuizDatasInit();

        if (options.Count != quizDatas.Count)
        {
            Debug.LogWarning("Init Btn list<> Failure");
            return;
        }

        HintTextInit();
        if (gameMode == MainMode.Exam) RandomPos();

        confirmBtn.onClick.AddListener(ConfirmBtn);
    }

    private void CheckMode()
    {
        if (GameController.Instance.mode == gameMode)
            return;

        gameMode = GameController.Instance.mode;
    }

    private void OnDisable()
    {
        confirmBtn.onClick.RemoveAllListeners();
    }

    #region Init
    private void OptionsInit()
    {
        if (optionPanel == null)
        {
            Debug.LogWarning("OptPanal is null");
            return;
        }

        options = new List<Button>();
        int i = 0;
        foreach (Button opt in optionPanel.GetComponentsInChildren<Button>())
        {
            int index = 0;
            index = i;
            options.Add(opt);
            opt.onClick.AddListener(() => OptBtnOnclick(index));
            i++;
        }
    }

    private void QuizDatasInit()
    {
        if (quizPanel == null)
        {
            Debug.LogWarning("quizPanel is null");
            return;
        }

        quizDatas = new List<QuizData>();
        int i = 0;
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
                data.button.onClick.AddListener(() => QuizBtnOnclick(index));
                data.optIndex = -1;
                quizDatas.Add(data);
                i++;
            }
        }
    }

    public virtual void HintTextInit()
    {
        if (options.Count <= 0)
            return;

        hintText = new List<string>();
        foreach (Button opt in options)
        {
            hintText.Add(opt.GetComponentInChildren<updatingMultiText>().currentString);
        }
    }

    private void RandomPos()
    {
        foreach (Transform t in optionPanel.GetComponentsInChildren<Transform>())
        {
            t.SetSiblingIndex(UnityEngine.Random.Range(0, options.Count));
        }
       
    }
    #endregion
    
    private void OptBtnOnclick(int index)
    {
        options[index].GetComponent<CanvasGroup>().alpha = 0;
        options[index].interactable = false;

        foreach (QuizData quizDatas in quizDatas)
        {
            if (!quizDatas.button.interactable)
            {
                quizDatas.optIndex = index;
                quizDatas.button.interactable = true;
                quizDatas.button.targetGraphic = options[index].GetComponent<Image>();
                quizDatas.button.GetComponentInChildren<Text>().text = options[index].GetComponentInChildren<Text>().text;
                quizDatas.button.GetComponentInChildren<Text>().color = Color.black;
                return;
            }
        }
    }

    private void QuizBtnOnclick(int index)
    {
        quizDatas[index].button.interactable = false;
        options[quizDatas[index].optIndex].GetComponent<CanvasGroup>().alpha = 1;
        options[quizDatas[index].optIndex].interactable = true;

        if (gameMode == MainMode.Train)
        {
            quizDatas[index].button.GetComponentInChildren<Text>().text = hintText[index];
        }
        else
        {
            quizDatas[index].button.GetComponentInChildren<Text>().text = "";
        }
    }

    public virtual void ConfirmBtn()
    {
        bool IfRight = true;
        int i = 0;
        foreach (QuizData data in quizDatas)
        {
            if (!data.button.interactable)
                return;

            if (data.optIndex != i)
            {
                if (gameMode == MainMode.Train)
                {
                    StartCoroutine(WrongAns(data.button.GetComponentInChildren<Text>().color));
                    return;
                }
                else
                {
                    IfRight = false;
                }
            }
            i++;
        }

        int s = IfRight ? 0 : 2;

        QuestManager.Instance.AddQuestCurrentAmount(goalType);
        QuestManager.Instance.MinusQuestScore(s);
        gameObject.SetActive(false);
    }

    private IEnumerator WrongAns(Color color)
    {
        int i = 0;
        while (i < 5)
        {
            color = Color.red;
            yield return new WaitForSeconds(.5f);
            color = Color.black;
            yield return new WaitForSeconds(.5f);
            i++;
        }
    }
}

