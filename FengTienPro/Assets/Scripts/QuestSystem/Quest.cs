using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Quest 
{
    public enum Status { WAITING,CHOOSABLE, CURRENT, DONE }
    public enum Name { None, Talk, WashHand, WashFood, CutFood, CookFood, FeedFood, CleanKit, FeedMeds, CleanMeds }

    [SerializeField]
    private string id;
    public string Id { get { return id; } }

    public QuestGiver giver;
    public Name questName;
    public string description;
    public int order;
    public bool isSingle;
    public Status status;
    public List<QuestPath> pathes;
    public List<QuestGoal> goals;
    public int score;

    public Quest (QuestGiver qg, Name n, string d, bool single ,int s ,List<QuestGoal> g)
    {
        id = Guid.NewGuid().ToString();
        giver = qg;
        questName = n;
        description = d;
        order = -1;
        isSingle = single;
        status = Status.WAITING;
        pathes = new List<QuestPath>();
        goals = g;
        goals[0].status = Goal.Status.CURRENT;
        score = s;
    }

    public void UpdateQuestStatus(Status es)
    {
        status = es;
        giver.UpdateStatus(es);
    }

    public void ResetQuestEvent()
    {
        foreach (QuestGoal g in goals)
        {
            g.status = Goal.Status.WAITING;
            g.currentAmount = 0;
        }
    }

    public void CheckGoals()
    {
        foreach (QuestGoal g in goals)
        {
            if (g.status == Goal.Status.DONE)
            { continue; }
            else
            {
                g.status = Goal.Status.CURRENT;
                return;
            }
        }
        Complete();
    }

    public void AddQuestCurrentAmount(Goal.Type gt)
    {
        foreach (QuestGoal g in goals)
        {
            if (g.type == gt && g.status == Goal.Status.CURRENT)
            {
                g.currentAmount++;
                GameController.Instance.currentPlayer.QuestStepCompleted();
                if (g.IsComplete())
                {
                    CheckGoals();
                }
                return;
            }
        }
    }

    public void Complete()
    {
        UpdateQuestStatus(Status.DONE);
        QuestManager.Instance.SetNextQuestStatus(this);
        GameController.Instance.score += score;
        GameController.Instance.AddtoRecord(goals);
    }

    public QuestGoal GetCurrentGoal()
    {
        foreach (QuestGoal g in goals)
        {
            if (g.status == Goal.Status.CURRENT)
                return g;
        }
        return null;
    }
}
