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
    public List<Quest> quests;
    [SerializeField]    private Dictionary<Goal.Type,QuestGoal> questGoals;
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
        GameController.Instance.gameMainInit += Set; 
        //TPManager.SetActive(true);
    }

    private void OnDestroy() => GameController.Instance.gameMainInit -= Set;

    public void QuestInit()
    {
        foreach (QuestGiver qg in GetComponentsInChildren<QuestGiver>())
        {
            quests.Add(qg.quest);
            foreach (QuestGoal goal in qg.quest.goals)
            {
                Debug.Log(goal.type);
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
        foreach (var q in quests)
        {
            if (q.state != Quest.State.WAITING)
                q.UpdateQuestStatus(Quest.State.WAITING);
        }
        quests[0].UpdateQuestStatus(Quest.State.CHOOSABLE);
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
    /// <summary>
    /// if Quest's State is Current then plus Amount
    /// </summary>
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
        if (GameController.Instance.mode == MainMode.Train)
            return;

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
                //Reset the Goals
                q.UpdateQuestStatus(Quest.State.CURRENT);
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
