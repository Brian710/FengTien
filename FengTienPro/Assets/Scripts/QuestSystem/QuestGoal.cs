using UnityEngine;
using System;

[Serializable]
public class QuestGoal
{
    public Goal goal;
    public Goal.Type type;
    public Goal.State state;
    public bool doItRight;
    public int currentAmount;
    public int requiredAmount;

    public QuestGoal(Goal.Type t, int r)
    {
        // default init stuff
        type = t;
        state = Goal.State.WAITING;
        doItRight = true;
        currentAmount = 0;
        requiredAmount = r;
    }

    public bool IsComplete()
    {
        if (currentAmount >= requiredAmount)
            UpdateGoalState(Goal.State.DONE);

        Debug.Log("Goal marked as completed.");
        return currentAmount >= requiredAmount;
    }

    public event Action<Goal.Type, Goal.State> OnGoalStateChange;

    public void UpdateGoalState(Goal.State state)
    {
        this.state = state;
        OnGoalStateChange?.Invoke(type, state);
    }


}
