public class AbilityExecutionSession
{
    public AbilityInstance AbilityInstance { get; }
    public ExecutionState State { get; private set; }
    public float CastProgress { get; private set; }
    public bool IsFinished { get; private set; }

    private float castTime;

    //DEBUG
    public int ExecutionId { get; }

    private static int globalId;

    public AbilityExecutionSession(AbilityInstance ability)
    {
        AbilityInstance = ability;
        State = ExecutionState.Running;
        castTime = ability.Definition.CastTime;
        ExecutionId = ++globalId;
    }

    public bool IsActive => State == ExecutionState.Running;

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
        Complete(false);
    }

    public void FinishSuccess()
    {
        if (!IsActive)
            return;

        State = ExecutionState.Succeeded;

        Complete(true);
    }

    private void Complete(bool success)
    {
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