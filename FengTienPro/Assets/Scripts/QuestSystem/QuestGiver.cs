﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [Header("UI")]
    public Button questBtn;

    [Header("Init Data")]
    public Quest.Name questName;
    public string questDescription;
    public bool isSingle;
    public int questScore;
    public List<Goal> goals;

    [Header("Quest GameObject")]
    public List<IObjControllerBase> InterObjs;

    [SerializeField]
    private Quest _quest;
    public Quest quest { get { return _quest; } }

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
        SetQuestLoc(true);
        questBtn.gameObject.SetActive(false);
        quest.UpdateQuestStatus(Quest.State.CURRENT);
        GameController.Instance.quest = quest;
    }

    private void SetQuestLoc(bool value)
    {
        if (InterObjs.Count <= 0)
            return;

        foreach (IObjControllerBase g in InterObjs)
        {
            g.SetInterObjActive(value);
        }
    }

    public void ReopenQuest()
    {
        SetQuestLoc(false);
        SetQuestLoc(true);
    }
}
