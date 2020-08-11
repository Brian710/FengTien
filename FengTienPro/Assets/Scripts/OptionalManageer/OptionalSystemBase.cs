using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MinYanGame.Core;

[RequireComponent(typeof(CanvasGroup))]
public class OptionalSystemBase : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private GameObject quizPanel;

    private List<Button> options;
    private List<QuizData> quizDatas;
    private List<string> correctAns;


    [SerializeField]
    private Button confirmBtn;

    [SerializeField]
    private Goal.Type goalType;

    private GameController gControl;

    private void OnEnable()
    {
        if(gControl)
            gControl = GameController.Instance;

        OptionsInit();
        QuizDatasInit();

        if (options.Count != quizDatas.Count)
        {
            Debug.LogWarning("Init Btn list<> Failure");
            return;
        }

        AnsInit();
        RandomPos();

        confirmBtn.onClick.AddListener(ConfirmBtn);
    }

    private void OnDisable()
    {
        confirmBtn.onClick.RemoveAllListeners();
    }

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
                if (gControl.mode == MainMode.Exam)
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

    public virtual void AnsInit()
    {
        correctAns = new List<string>();

        if (quizDatas.Count > 0)
        {
            foreach (QuizData opt in quizDatas)
            {
                correctAns.Add(opt.button.GetComponentInChildren<updatingMultiText>().currentString);
            }
        }
    }

    private void RandomPos()
    {
        if (gControl.mode == MainMode.Exam)
        {
            foreach (Transform t in optionPanel.GetComponentsInChildren<Transform>())
            {
                t.SetSiblingIndex(Random.Range(0, options.Count));
            }
        }
    }

    private void OptBtnOnclick(int index)
    {
        options[index].GetComponent<CanvasGroup>().alpha = 0;
        options[index].interactable = false;

        foreach (QuizData data in quizDatas)
        {
            if (!data.button.interactable)
            {
                data.optIndex = index;
                data.button.interactable = true;
                data.button.GetComponentInChildren<Text>().text = options[index].GetComponentInChildren<Text>().text;
                data.button.GetComponentInChildren<Text>().color = Color.black;
                return;
            }
        }
    }

    private void QuizBtnOnclick(int index)
    {
        quizDatas[index].button.interactable = false;
        options[quizDatas[index].optIndex].GetComponent<CanvasGroup>().alpha = 1;
        options[quizDatas[index].optIndex].interactable = true;

        if (gControl.mode == MainMode.Train)
        {
            if (correctAns.Count >= 0)
                quizDatas[index].button.GetComponentInChildren<Text>().text = correctAns[index];
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
                if (gControl.mode == MainMode.Train)
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

