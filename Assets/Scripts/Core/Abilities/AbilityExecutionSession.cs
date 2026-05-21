using System;

public class AbilityExecutionSession
{
    public AbilityInstance AbilityInstance { get; }
    public ExecutionState State { get; private set; }
    public float CastProgress { get; private set; }
    public bool IsFinished { get; private set; }

    public Action<AbilityExecutionSession> Finished;
    public event Action<AbilityCastStartedEvent> CastStartedEvent;
    public event Action<AbilityCastInterruptedEvent> CastInterruptedEvent;
    public event Action<AbilityCastFinishedEvent> CastFinishedEvent;

    private float castTime;

    //DEBUG
    public int ExecutionId { get; }

    private static int globalId;

    public AbilityExecutionSession(AbilityInstance ability)
    {
        AbilityInstance = ability;
        castTime = ability.Definition.CastTime;
        ExecutionId = ++globalId;
    }

    public bool IsActive => State == ExecutionState.Running;

    public void StartCast()
    {
        State = ExecutionState.Running;
        CastStartedEvent?.Invoke(new AbilityCastStartedEvent(this));
    }

    public void Tick(float deltaTime)
    {
        if (!IsActive)
            return;

        CastProgress += deltaTime;
    }

    public float GetProgress01()
    {
        if (castTime <= 0f)
            return 1f;

        return CastProgress / castTime;
    }

    public void Interrupt(InterruptType sourceInterruptType)
    {
        if (!IsActive)
            return;

        if (!AbilityInstance.Definition.CanBeInterruptedBy(sourceInterruptType))
            return;

        State = ExecutionState.Interrupted;

        CastInterruptedEvent?.Invoke(new AbilityCastInterruptedEvent(this));

        Complete(false);
    }

    public void FinishSuccess()
    {
        if (!IsActive)
            return;

        State = ExecutionState.Succeeded;

        CastFinishedEvent?.Invoke(new AbilityCastFinishedEvent(this));

        Complete(true);
    }

    private void Complete(bool success)
    {
        Finished?.Invoke(this);
        AbilityInstance.OnSessionFinished();

        if (success)
        {
            AbilityInstance.StartCooldown();
        }
    }
}

public enum ExecutionState
{
    Running,
    Interrupted,
    Succeeded,
    Failed
}