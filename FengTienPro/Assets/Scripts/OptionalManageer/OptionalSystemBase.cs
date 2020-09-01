using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionalSystemBase : MonoBehaviour
{
    [SerializeField]
    protected Goal.Type goalType;
    [SerializeField]
    protected GameObject OPCanvas;
    [SerializeField]
    protected GameObject optionPanel;
    [SerializeField]
    protected GameObject quizPanel;
    [SerializeField]
    protected Button confirmBtn;

    protected List<Button> options;
    protected List<QuizData> quizDatas;
    protected List<string> hintText;

    protected MainMode mode;

    private void Start()
    {
        OptionsInit();
        QuizDatasInit();
        HintTextInit();
        Debug.Log(QuestManager.Instance.GetQuestGoalByType(goalType));
        QuestManager.Instance.GetQuestGoalByType(goalType).OnGoalStateChange += OnGoalStateChange;
        OPCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(goalType).OnGoalStateChange -= OnGoalStateChange;
    }

    private void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        //DoubleCheck
        if (type != goalType)
            return;

        switch (state)
        {
            case Goal.State.WAITING:
                OpenCanv(false);
                break;
            case Goal.State.CURRENT:
                OpenCanv(true);
                break;
            case Goal.State.DONE:
                break;
        }
    }
    public virtual void OpenCanv(bool value)
    {
        if (value)
        {
            OPCanvas.SetActive(value);
            mode = GameController.Instance.mode;
            RandomPos();
            confirmBtn.onClick.AddListener(ConfirmBtn);
        }
        else
        {
            confirmBtn.onClick.RemoveAllListeners();
            OPCanvas.SetActive(value);
        }
    }

    #region Canvas Init
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
                if (mode == MainMode.Exam)
                {
                    t.GetComponentInChildren<Text>().text = "";
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
            hintText.Add(opt.GetComponentInChildren<Text>().text);
        }
    }

    public virtual void RandomPos()
    {
        foreach (Button t in options)
        {
            if (mode == MainMode.Exam)
                t.transform.SetSiblingIndex(Random.Range(0, options.Count));
            else
                t.transform.SetAsLastSibling();
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
        quizDatas[index].button.GetComponentInChildren<Text>().text = mode == MainMode.Train ? hintText[index] : "";
    }

    public virtual void ConfirmBtn()
    {
        int i = 0;
        foreach (QuizData data in quizDatas)
        {
            if (!data.button.interactable)
                return;

            if (data.optIndex != i)
            {
                QuestManager.Instance.MinusQuestScore(2);
                StartCoroutine(WrongAns(data.button.GetComponentInChildren<Text>()));
                return;
            }
            i++;
        }
        QuestManager.Instance.AddQuestCurrentAmount(goalType);
        OpenCanv(false);
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

