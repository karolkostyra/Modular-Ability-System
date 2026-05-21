using UnityEngine;
using Cysharp.Threading.Tasks;

public class AbilityExecutor
{
    public AbilityExecutor()
    {
    }

    public async UniTask Execute(AbilityContext context, AbilityExecutionSession session)
    {
        if (context == null)
            return;

        if (session == null)
            return;

        var ability = context.AbilityInstance;

        float duration = ability.Definition.CastTime;
        float t = 0f;

        session.StartCast();

        while (t < duration)
        {
            await UniTask.Yield();

            if (!session.IsActive)
                return;

            t += Time.deltaTime;
            session.Tick(Time.deltaTime);
        }

        if (!session.IsActive)
            return;

        var targets = ability.Definition.TargetResolver
                                        .ResolveTargets(context);

        foreach (var effect in ability.Definition.Effects)
        {
            effect.Apply(context);
        }

        session.FinishSuccess();
    }
}