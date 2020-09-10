using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

[Serializable]
public class Quest 
{
    public enum State { WAITING, CHOOSABLE, CURRENT, DONE }
    public enum Name { None, Talk, WashHand, WashFood, CutFood, CookFood, FeedFood, CleanKit, FeedMeds, CleanMeds ,Entrance}

    [SerializeField]    private string id;
    public string Id => id;

    public QuestGiver giver;
    public Name qName;
    public string description;
    public int order;
    public bool isSingle;
    public State state;
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
        Debug.LogWarning(state.ToString() + "  "+ OnQuestChange);
        //call for the things update not in the quest system
        OnQuestChange ?.Invoke(qName, state);

        switch (state)
        {
            case State.WAITING:
                ResetAllGoals();
                break;
            case State.CHOOSABLE:
                giver.OpenQuestBtn(true);
                break;
            case State.CURRENT:
                ResetAllGoals();
                goals[0].UpdateGoalState(Goal.State.CURRENT);
                break;
            case State.DONE:
                break;
        }
    }

    public void ResetAllGoals()
    {
        foreach (QuestGoal g in goals)
        {
            g.UpdateGoalState(Goal.State.WAITING);
            g.currentAmount = 0;
        }
    }

    public void AddCurrentGoalAmount(Goal.Type gt)
    {
        foreach (QuestGoal g in goals)
        {
            if (g.type == gt)
            {
                if (g.state == Goal.State.CURRENT)
                {
                    g.currentAmount++;
                    GameController.Instance.currentPlayer.QuestGoalCompleted();
                    if (g.IsComplete())
                    {
                        CheckGoals();
                    }
                    return;
                }
            }
        }
    }
    private void CheckGoals()
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
    private void Complete()
    {
        GameController.Instance.score += score;
        GameController.Instance.AddtoRecord(goals);

        UpdateQuestStatus(State.DONE);
        giver.SetQuestLoc(false, 1.5f);
        PlayerController.Instance.QuestCompleted();
        QuestManager.Instance.SetNextQuestStatus(this);
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
