﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region singleton

    public static QuestManager Instance;
    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        firstInit = true;
    }
    #endregion
    [SerializeField]
    private bool firstInit;
    public List<Quest> quests;

    public void AddtoQuestlist(Quest q)
    {
        quests.Add(q);
    }
    
    private void Start()
    {
        QuestInit();
        BFS(quests[0]);
        //quests[0].UpdateQuestStatus(Quest.Status.CHOOSABLE);
        PrintPath();
        Set();
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
            if (q.questName == Quest.Name.Talk)
            {
                q.UpdateQuestStatus(Quest.Status.CHOOSABLE);
            }
            else
            {
                q.ResetQuestEvent();
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
            Debug.LogWarning(qe.questName + ", order: " + qe.order);
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

    public void SetNextQuestStatus(Quest cq)
    {

        foreach (Quest q in quests)
        {
            if (q.order == cq.order + 1)
            {
                    q.UpdateQuestStatus(Quest.Status.CHOOSABLE);
            }
        }
    }

    public void AddQuestCurrentAmount( Goal.Type gt)
    {
        foreach (Quest q in quests)
        {
            if (q.status == Quest.Status.CURRENT)
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
            if (q.status == Quest.Status.CURRENT)
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
            if (q.status == Quest.Status.CURRENT)
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

    public Quest FindChoosableQuest()
    {
        foreach (Quest q in quests)
        {
            if (q.status == Quest.Status.CHOOSABLE)
                return q;
        }

        return null;
    }

    public Goal.Type GetcurrentGoal()
    { 
        foreach(Quest q in quests)
        {
            if (q.status == Quest.Status.CURRENT)
            {
                foreach (QuestGoal goal in q.goals)
                {
                    if (goal.status == Goal.Status.CURRENT)
                        return goal.type;
                }
            }
        }
        return Goal.Type.None;
    }

}
