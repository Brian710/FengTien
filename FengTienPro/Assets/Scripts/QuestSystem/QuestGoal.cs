using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class QuestGoal
{
    public Goal goal;
    public Goal.Type type;
    public Goal.Status status;
    public bool doItRight;
    public int currentAmount;
    public int requiredAmount;

    public QuestGoal(Goal.Type t, int r)
    {
        // default init stuff
        type = t;
        status = Goal.Status.WAITING;
        doItRight = true;
        currentAmount = 0;
        requiredAmount = r;
    }

    public bool IsComplete()
    {
        if(currentAmount >= requiredAmount)
            status = Goal.Status.DONE;

        Debug.Log("Goal marked as completed.");
        return currentAmount >= requiredAmount;
    }
}
