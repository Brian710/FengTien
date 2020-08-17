using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [Header("UI")]
    public Button questBtn;

    [Header("Init Data")]
    public Quest.Name questName;
    public string questDescription;
    public bool isSingle;
    public int questScore;
    public List<Goal> goals;

    [Header("Quest GameObject")]
    public List<GameObject> gameObjects;

    [SerializeField]
    private Quest _quest;
    public Quest quest { get { return _quest; } }

    public delegate void OnQuestAcceptDelegate(bool active );
    public static event OnQuestAcceptDelegate OnQuestAcceptListener;

    public delegate void OnQuestCompleteDelegate(bool active);
    public static event OnQuestCompleteDelegate OnQuestCompleteListener;

    private void Awake()
    {
        List<QuestGoal> newgoals = new List<QuestGoal>();
        foreach (Goal g in goals)
        {
            QuestGoal newgoal = new QuestGoal(g.type, g.requiredAmount);
            newgoals.Add(newgoal);
        }
        _quest = new Quest(this, questName, questDescription, isSingle, questScore, newgoals);
    }

    private void Start()
    {
        QuestManager.Instance.AddtoQuestlist(_quest);
    }

    public void OpenQuestWindow()
    {
        questBtn.gameObject.SetActive(true);
        questBtn.onClick.AddListener(AcceptQuest);
    }

    public void AcceptQuest()
    {
        quest.UpdateQuestStatus(Quest.Status.CURRENT);
        questBtn.gameObject.SetActive(false);
        GameController.Instance.quest = quest;
        //init Quest Obj
        SetQuestLoc(true);
    }

    private void SetQuestLoc(bool value)
    {
        if (gameObjects.Count <= 0)
            return;

        foreach (GameObject g in gameObjects)
        {
            g.SetActive(value);
        }
    }
    //quest status will change this status too
    public void UpdateStatus(Quest.Status es)
    {
        //ChangeUI
        switch (es)
        {
            case Quest.Status.CHOOSABLE:
                OpenQuestWindow();
                break;
            case Quest.Status.CURRENT:
                OnQuestAcceptListener?.Invoke(false);
                break;
            case Quest.Status.DONE:
                OnQuestCompleteListener?.Invoke(true);
                SetQuestLoc(false);
                break;
        }
    }
}
