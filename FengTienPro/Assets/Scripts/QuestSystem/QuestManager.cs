using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region singleton

    public static QuestManager Instance;
    protected  void InitSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    #endregion
    public List<Quest> quests;
    public Dictionary<Goal.Type,QuestGoal> questGoals;
    public Quest currentQuest;
    //public GameObject TPManager;
    protected virtual void Awake()
    {
        InitSingleton();
        quests = new List<Quest>();
        questGoals = new Dictionary<Goal.Type, QuestGoal>();
    }
    
    private void Start()
    {
        QuestInit();
        BFS(quests[0]);
        //PrintPath();
        Set();
        //TPManager.SetActive(true);
    }
    public void QuestInit()
    {
        foreach (QuestGiver qg in GetComponentsInChildren<QuestGiver>())
        {
            quests.Add(qg.quest);
            foreach (QuestGoal goal in qg.quest.goals)
            {
                questGoals.Add(goal.type, goal);
            }
        }

        for (int i = 0; i < quests.Count - 1; i++)
        {
            AddPath(quests[i].Id, quests[i + 1].Id);
        }
    }

    public void Set()
    {
        Debug.LogWarning("QuestManager Set()");
        foreach (var q in quests)
        {
            if (q.qName == Quest.Name.Talk)
            {
                q.UpdateQuestStatus(Quest.State.CHOOSABLE);
                Debug.LogWarning("Talk Set()");
            }
            else
            {
                q.UpdateQuestStatus(Quest.State.WAITING);
                Debug.LogWarning("Other Set()");
            }
        }
        currentQuest = FindCurrentQuest();
    }

    public void AddPath(string fromQE, string toQE)
    {
        Quest from = FindQuestbyID(fromQE);
        Quest to = FindQuestbyID(toQE);
        if (from != null && to != null)
        {
            QuestPath p = new QuestPath(from, to);
            from.pathes.Add(p);
        }
    }

    public Quest FindQuestbyID(string Id)
    {
        foreach (var q in quests)
        {
            if (q.Id == Id)
                return q;
        }

        return null;
    }


    public void BFS(Quest q, int orderNum = 1)
    {
        q.order = orderNum;

        foreach (QuestPath p in q.pathes)
        {
            if (p.endEvent.order == -1)
                BFS(p.endEvent, orderNum + 1);
        }
    }

    public void PrintPath()
    {
        foreach (var qe in quests)
        {
            Debug.LogWarning(qe.qName + ", order: " + qe.order);
        }
    }

    

    //mainly for Teleport
    public void SetNextQuestStatus(Quest cq)
    {
        foreach (var q in quests)
        {
            if (q.order == cq.order + 1)
            {
                q.UpdateQuestStatus(Quest.State.CHOOSABLE);
               
            }
        }
    }

    public void AddQuestCurrentAmount( Goal.Type gt)
    {
        foreach (var q in quests)
        {
            if (q.state == Quest.State.CURRENT)
            {
                q.AddCurrentGoalAmount(gt);
                break;
            }
        }
    }

    public void MinusQuestScore(int delta)
    {
        foreach (var q in quests)
        {
            if (q.state == Quest.State.CURRENT)
            {
                q.GetCurrentGoal().doItRight = false;
                q.score -= delta;
                if (q.score <= 0)
                    q.score = 0;
                return;
            }
        }
    }

    public void ReopenQuestGiver()
    {
        foreach (var q in quests)
        {
            if (q.state == Quest.State.CURRENT)
            {
                foreach (QuestGoal goal in q.goals)
                {
                    goal.currentAmount = 0;
                }
                q.giver.ReopenQuest();
                break;
            }
        }
    }

    private Quest FindCurrentQuest()
    {
        foreach (var q in quests)
        {
            if (q.state == Quest.State.CURRENT)
                return q;
        }

        return FindChoosableQuest();
    }
    public Quest FindChoosableQuest()
    {
        foreach (var q in quests)
        {
            if (q.state == Quest.State.CHOOSABLE)
                return q;
        }

        return null;
    }

    public QuestGoal GetQuestGoalByType(Goal.Type t)
    {
        if(questGoals.ContainsKey(t))
            return questGoals[t];

        return null;
    }

    public Quest GetQuestByName(Quest.Name name)
    {
        foreach (Quest q in quests)
        {
            if (q.qName == name)
            {
                return q;
            }
        }

        return null;
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (Application.isPlaying)
        {
            currentQuest.AddCurrentGoalAmount(currentQuest.GetCurrentGoal().type);
        }
    }
#endif
}
