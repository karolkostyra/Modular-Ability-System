using UnityEngine;

[CreateAssetMenu]
public class ApplyStatusAction : AbilityActionDefinition
{
    [SerializeField] private StatusDefinition status;

    public override void Execute(AbilityContext context)
    {
        foreach (var target in context.Targets)
        {
            target.ApplyStatus(status);
        }
    }
}