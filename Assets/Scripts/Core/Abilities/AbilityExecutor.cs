using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class AbilityExecutor
{
    public AbilityExecutor()
    {
    }

    public async UniTask Execute(AbilityContext context)
    {
        if (context == null)
            return;

        var ability = context.AbilityInstance;

        var session = ability.CreateExecutionSession();
        if (session == null)
            return;

        float duration = ability.Definition.CastTime;
        float t = 0f;

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