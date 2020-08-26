using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Quest 
{
    public enum State { WAITING,CHOOSABLE, CURRENT, DONE }
    public enum Name { None, Talk, WashHand, WashFood, CutFood, CookFood, FeedFood, CleanKit, FeedMeds, CleanMeds ,Entrance}

    [SerializeField]
    private string id;
    public string Id { get { return id; } }

    public QuestGiver giver { get; private set; }
    public Name qName { get; private set; }
    public string description { get; private set; }
    public int order { get; set; }
    public bool isSingle { get; private set; }
    public State state { get;  private set; }
    public List<QuestPath> pathes;
    public List<QuestGoal> goals;
    public int score;

    public Quest (QuestGiver qg, Name n, string d, bool single ,int s ,List<QuestGoal> g)
    {
        id = Guid.NewGuid().ToString();
        giver = qg;
        qName = n;
        description = d;
        order = -1;
        isSingle = single;
        state = State.WAITING;
        pathes = new List<QuestPath>();
        goals = g;
        score = s;
    }

    public event Action<Name,State> OnQuestChange;

    public void UpdateQuestStatus(State es)
    {
        state = es;
        //call for the things update not in the quest system
        OnQuestChange ?.Invoke(qName, state);

        switch (state)
        {
            case State.WAITING:
                ResetAllQuests();
                break;
            case State.CHOOSABLE:
                giver.OpenQuestBtn(true);
                break;
            case State.CURRENT:
                goals[0].UpdateGoalState(Goal.State.CURRENT);
                break;
            case State.DONE:
                break;
        }
    }

    public void ResetAllQuests()
    {
        foreach (QuestGoal g in goals)
        {
            g.UpdateGoalState(Goal.State.WAITING);
            g.currentAmount = 0;
        }
    }

    public void CheckGoals()
    {
        foreach (QuestGoal g in goals)
        {
            if (g.state == Goal.State.WAITING)
            {
                g.UpdateGoalState(Goal.State.CURRENT);
                return;
            }
        }
        Complete();
    }

    public void AddQuestCurrentAmount(Goal.Type gt)
    {
        foreach (QuestGoal g in goals)
        {
            if (g.type == gt && g.state == Goal.State.CURRENT)
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
        UpdateQuestStatus(State.DONE);
        QuestManager.Instance.SetNextQuestStatus(this);
        GameController.Instance.score += score;
        GameController.Instance.AddtoRecord(goals);
    }

    public QuestGoal GetCurrentGoal()
    {
        foreach (QuestGoal g in goals)
        {
            if (g.state == Goal.State.CURRENT)
                return g;
        }
        return null;
    }
}
