using System;
using System.Collections.Generic;
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
    [SerializeField]
    private bool firstInit;
    public List<Quest> quests;
    public Quest CurrentQuest { get; private set; }

    public void AddtoQuestlist(Quest q)
    {
        quests.Add(q);
    }

    protected virtual void Awake()
    {
        InitSingleton();
        firstInit = true;
    }
    private void Start()
    {
        QuestInit();
        BFS(quests[0]);
        PrintPath();
        Set();
        CurrentQuest = FindCurrentQuest();
        firstInit = false;
    }

    private void OnEnable()
    {
        if (firstInit)
        {
            return;
        }
        Set();
    }

    private void Set()
    {
        foreach (Quest q in quests)
        {
            if (q.qName == Quest.Name.Talk)
            {
                q.UpdateQuestStatus(Quest.State.CHOOSABLE);
            }
            else
            {
                q.UpdateQuestStatus(Quest.State.WAITING);
            }
        }
    }

    public void AddPath(string fromQE, string toQE)
    {
        Quest from = FindQuestEvent(fromQE);
        Quest to = FindQuestEvent(toQE);
        if (from != null && to != null)
        {
            QuestPath p = new QuestPath(from, to);
            Debug.Log(from + " ; " + from.pathes + " ; " + p);
            from.pathes.Add(p);
        }
    }

    private Quest FindQuestEvent(string id)
    {
        foreach (Quest quest in quests)
        {
            if (quest.Id == id)
            {
                return quest;
            }
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
        foreach (Quest qe in quests)
        {
            Debug.LogWarning(qe.qName + ", order: " + qe.order);
        }
    }

    public void QuestInit()
    {
        quests = new List<Quest>();
        foreach (QuestGiver qg in GetComponentsInChildren<QuestGiver>())
        {
            AddtoQuestlist(qg.quest);
        }

        for (int i = 0; i < quests.Count - 1; i++)
        {
            AddPath(quests[i].Id, quests[i + 1].Id);
        }
    }

    //mainly for Teleport
    public void SetNextQuestStatus(Quest cq)
    {
        foreach (Quest q in quests)
        {
            if (q.order == cq.order + 1)
            {
                q.UpdateQuestStatus(Quest.State.CHOOSABLE);
                q.giver.OpenQuestWindow(true);
            }
        }
    }

    public void AddQuestCurrentAmount( Goal.Type gt)
    {
        foreach (Quest q in quests)
        {
            if (q.state == Quest.State.CURRENT)
            {
                q.AddQuestCurrentAmount(gt);
                break;
            }
        }
    }

    public void MinusQuestScore(int delta)
    {
        foreach (Quest q in quests)
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
        foreach (Quest q in quests)
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
        foreach (Quest q in quests)
        {
            if (q.state == Quest.State.CURRENT)
                return q;
        }

        return FindChoosableQuest();
    }
    public Quest FindChoosableQuest()
    {
        foreach (Quest q in quests)
        {
            if (q.state == Quest.State.CHOOSABLE)
                return q;
        }

        return null;
    }

    public Goal.Type GetcurrentGoal()
    { 
        foreach(Quest q in quests)
        {
            if (q.state == Quest.State.CURRENT)
            {
                foreach (QuestGoal goal in q.goals)
                {
                    if (goal.state == Goal.State.CURRENT)
                        return goal.type;
                }
            }
        }
        return Goal.Type.None;
    }

}
