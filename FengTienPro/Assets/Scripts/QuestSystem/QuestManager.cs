using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
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
    }
    #endregion


    public void AddtoQuestlist(Quest q)
    {
        quests.Add(q);
    }
    void Start()
    {
        QuestInit();
        BFS(quests[0]);
        quests[0].UpdataQuestEvent(Quest.Status.CHOOSABLE);
        PrintPath();
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
        if (quests.Count < 2)
            return;

        for (int i = 0; i < quests.Count - 1; i++)
        {
            AddPath(quests[i].Id, quests[i + 1].Id);
        }
    }

    public void UpdateQuestsOnCompletion(Quest qe)
    {
        foreach (Quest q in quests)
        {
            if (q.order == qe.order + 1)
            {
                if (qe.giver.isAuto)
                    q.giver.AcceptQuest();
                else
                    q.UpdataQuestEvent(Quest.Status.CHOOSABLE);
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
                q.score -= delta;
                if (q.score <= 0)
                    q.score = 0;
                return;
            }
        }
    }
}
