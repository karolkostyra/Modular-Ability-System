using UnityEngine;

public class GameplayInstaller : MonoBehaviour
{
    [SerializeField] private AbilitySystem abilitySystem;

    private GameplayEventBus eventBus;
    private AbilityExecutor executor;
    private AbilitySessionEventRouter abilitySessionEventRouter;
    private CombatLogger combatLogger;

    private void Awake()
    {
        eventBus = new GameplayEventBus();
        executor = new AbilityExecutor();
        abilitySessionEventRouter = new AbilitySessionEventRouter(eventBus);
        combatLogger = new CombatLogger(eventBus);

        abilitySystem.Initialize(executor, abilitySessionEventRouter);
    }
}