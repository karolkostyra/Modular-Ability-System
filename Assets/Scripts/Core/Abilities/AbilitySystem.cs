using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    [SerializeField] private List<AbilityDefinition> abilityDefinitions;

    private List<AbilityInstance> abilities;
    private AbilityExecutor executor;

    private AbilityInstance activeAbility;

    private void Awake()
    {
        abilities = abilityDefinitions.Select(x => new AbilityInstance(x))
                                      .ToList();

        executor = new AbilityExecutor();
    }

    private void Update()
    {
        foreach (var ability in abilities)
        {
            ability.Tick(Time.deltaTime);
        }
    }

    public void TryCast(int index)
    {
        if (abilities.Count <= index)
            return;

        var abilityInstance = abilities[index];

        if (!abilityInstance.CanCast())
        {
            Debug.Log("Cannot cast ability");
            return;
        }

        activeAbility = abilityInstance;

        var context = new AbilityContext
        {
            Caster = gameObject,
            Origin = transform.position,
            AbilityInstance = abilityInstance
        };

        executor.Execute(context).Forget();
    }

    public bool TryInterrupt(AbilityInstance ability, InterruptType interruptType)
    {
        var session = ability.ActiveSession;

        if (session == null)
            return false;

        session.Interrupt(interruptType);
        return true;
    }


    //DEBUG
    public void InterruptActiveAbility()
    {
        if (activeAbility == null)
            return;

        TryInterrupt(activeAbility, InterruptType.Hard);
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            wordWrap = true,
            fontSize = 24
        };

        float y = 10f;

        for (int i = 0; i < abilities.Count; i++)
        {
            var ability = abilities[i];

            string text = $"{ability.Definition.Id}\n" +
                          $"Cooldown: {ability.CooldownRemaining:F1}";

            if (ability.ActiveSession != null)
            {
                text += $"\nCastProgress: {ability.ActiveSession.CastProgress:F1} ({ability.Definition.CastTime})\n" +
                        $"WasInterrupted: {ability.ActiveSession.State == ExecutionState.Interrupted}";
            }

            Vector2 size = style.CalcSize(new GUIContent(text));

            float width = 300f;
            float height = style.CalcHeight(new GUIContent(text), width);

            GUI.Label(new Rect(10f, y, width, height), text, style);

            y += height + 10f;
        }
    }
}