using System;

public interface IGoal
{
    event Action onTick;
    event Action onEnter;
    event Action onExit;
    void Tick();
    void Enter();
    void Exit();
}
