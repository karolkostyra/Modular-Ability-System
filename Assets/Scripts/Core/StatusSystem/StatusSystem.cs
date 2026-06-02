using System.Collections.Generic;

public class StatusSystem
{
    private readonly List<StatusInstance> activeStatuses = new();

    public void Apply(StatusDefinition definition)
    {
        activeStatuses.Add(new StatusInstance(definition));
    }

    public void Tick(float deltaTime)
    {
    }
}