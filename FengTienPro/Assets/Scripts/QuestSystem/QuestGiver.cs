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
        //QuestManager.Instance.AddtoQuestlist(_quest);
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

    public void ReopenQuest()
    {
        SetQuestLoc(false);
        SetQuestLoc(true);
    }

    

    //quest status will change this status too
    public void UpdateStatus(Quest.Status es)
    {
        //ChangeUI
        switch (es)
        {
            case Quest.Status.CHOOSABLE:
                OpenQuestWindow();
                TeleportManager.Instance.ShowTPbyGoal(quest.questName);
                TeleportManager.Instance.ShowTPbyGoal(Quest.Name.None);
                TeleportManager.Instance.ShowTPbyGoal(Quest.Name.Entrance);
                break;
            case Quest.Status.CURRENT:
                TeleportManager.Instance.HideTPbyGoal(Quest.Name.None);
                TeleportManager.Instance.HideTPbyGoal(Quest.Name.Entrance);
                break;
            case Quest.Status.DONE:
                TeleportManager.Instance.ShowTPbyGoal(quest.questName);
                SetQuestLoc(false);
                break;
        }
    }
}
