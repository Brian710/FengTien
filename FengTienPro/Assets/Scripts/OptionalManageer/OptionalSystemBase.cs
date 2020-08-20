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
    protected Goal.Type goalType;
    [SerializeField]
    protected GameObject optionPanel;
    [SerializeField]
    protected GameObject quizPanel;
    [SerializeField]
    protected Button confirmBtn;

    protected List<Button> options;
    protected List<QuizData> quizDatas;
    protected List<string> hintText;

    protected MainMode gameMode;

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

    public virtual void QuizDatasInit()
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

    public virtual void OptBtnOnclick(int index)
    {
        options[index].GetComponent<CanvasGroup>().alpha = 0;
        options[index].interactable = false;

        foreach (QuizData quizData in quizDatas)
        {
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

    public void QuizBtnOnclick(int index)
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
                    StartCoroutine(WrongAns(data.button.GetComponentInChildren<Text>()));
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

    protected IEnumerator WrongAns(Text text)
    {
        int i = 0;
        while (i < 5)
        {
            text.color = Color.red;
            yield return new WaitForSeconds(.5f);
            text.color = Color.black;
            yield return new WaitForSeconds(.5f);
            i++;
        }
    }
}

