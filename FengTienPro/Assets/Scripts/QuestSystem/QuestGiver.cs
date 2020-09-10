using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [Header("UI")]
    public Button questBtn;
    public GameObject OSCanv;

    [Header("Init Data")]
    public Quest.Name questName;
    public string questDescription;
    public bool isSingle;
    public int questScore;
    public List<Goal> goals;

    [Header("Goal GameObjects")]
    public List<IObjControllerBase> InterObjs;

    [SerializeField]    private Quest _quest;
    public Quest quest => _quest;

    private void Awake()
    {
        List<QuestGoal> newgoals = new List<QuestGoal>();
        foreach (Goal g in goals)
        {
            QuestGoal newgoal = new QuestGoal(g.type, g.requiredAmount);
            newgoals.Add(newgoal);
        }
        _quest = new Quest(this, questName, questDescription, isSingle, questScore, newgoals);
    }

    private void Start()
    {
        questBtn.gameObject.SetActive(false);
    }

    public void OpenQuestBtn(bool value)
    {
        if (value)
        {
            questBtn.gameObject.SetActive(value);
            questBtn.onClick.AddListener(AcceptQuest);
        }
        else
        {
            questBtn.gameObject.SetActive(value);
            questBtn.onClick.RemoveListener(AcceptQuest);
        }
    }

    //mainly for Teleport
    public void AcceptQuest()
    {
        //init Quest Obj
        SetQuestLoc(true,0f);
        OpenQuestBtn(false);
        quest.UpdateQuestStatus(Quest.State.CURRENT);
        GameController.Instance.quest = quest;
    }

    public void SetQuestLoc(bool value ,float delay )
    {
        if (InterObjs.Count <= 0)
            return;
        if (value)
        {
            foreach (IObjControllerBase g in InterObjs)
            {
                g.SetChildObjActive(true);
            }
        }
        else
            StartCoroutine(DelaySetLocInActive(delay));
    }

    public IEnumerator DelaySetLocInActive(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (IObjControllerBase g in InterObjs)
        {
            g.SetChildObjActive(false);
        }
    }

    public void OpenCanV(bool value)
    {
        if (OSCanv)
            OSCanv.SetActive(value);
    }
}