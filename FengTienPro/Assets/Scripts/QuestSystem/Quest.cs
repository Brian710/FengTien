using UnityEngine;
using MinYanGame.Core;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class Quest 
{
    public enum Status { WAITING,CHOOSABLE, CURRENT, DONE }

    [SerializeField]
    private string id;
    public string Id { get { return id; } }

    public QuestGiver giver;
    public string questName;
    public string description;
    public int order;
    public bool isSingle;
    public Status status;
    public List<QuestPath> pathes;
    public List<QuestGoal> goals;
    public int score;

    public Quest (QuestGiver qg, string n, string d, bool single ,int s ,List<QuestGoal> g)
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

    public void UpdataQuestEvent(Status es)
    {
        status = es;
        giver.UpdataEventStatus(es);
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
        if (goals.Count > 1)
        {
            for (int i = 0; i < goals.Count; i++)
            {
                QuestGoal g = goals[i];
                if (g.type == gt && g.status == Goal.Status.CURRENT)
                {
                    g.currentAmount++;
                    GameController.Instance.currentPlayer.QuestStepCompleted();
                    if (g.IsComplete())
                    {
                        CheckGoals();
                    }
                }
            }
        }
    }

    public void Complete()
    {
        UpdataQuestEvent(Status.DONE);
        QuestManager.Instance.UpdateQuestsOnCompletion(this);
        GameController.Instance.score += score;
    }
}
