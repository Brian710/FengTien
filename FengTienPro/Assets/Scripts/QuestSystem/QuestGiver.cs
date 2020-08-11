using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [Header("UI")]
    public Button questBtn;


    [Header("Init Data")]
    public string questName;
    public string questDescription;
    public bool isSingle;
    public int questScore;
    public List<Goal> goals;
    
    public bool isAuto;


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
        _quest = new Quest(this, questName, questDescription, isSingle,questScore, newgoals);
        QuestManager.Instance.AddtoQuestlist(_quest);
    }

    public void OpenQuestWindow()
    {
        questBtn.gameObject.SetActive(true);
        questBtn.onClick.AddListener(AcceptQuest);
    }

    public void AcceptQuest()
    {
        quest.UpdataQuestEvent(Quest.Status.CURRENT);
        GameController.Instance.quest = quest;
        //init Quests
        InitQuestLoc();
    }

    private void InitQuestLoc()
    {
        
    }


    public void UpdataEventStatus(Quest.Status es)
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
                break;
        }
    }
}
