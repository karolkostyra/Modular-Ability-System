using UnityEngine;

public class GameplayInstaller : MonoBehaviour
{
    [SerializeField] private AbilitySystem abilitySystem;

    private GameplayEventBus eventBus;
    private AbilityExecutor executor;
    private AbilitySessionEventRouter abilitySessionEventRouter;
    private StatusEventRouter statusEventRouter;
    private CombatLogger combatLogger;

    private void Awake()
    {
        eventBus = new GameplayEventBus();
        executor = new AbilityExecutor();
        abilitySessionEventRouter = new AbilitySessionEventRouter(eventBus);
        statusEventRouter = new StatusEventRouter(eventBus);
        combatLogger = new CombatLogger(eventBus);

        abilitySystem.Initialize(executor, abilitySessionEventRouter);

        InitializeCharacters();
    }

    private void InitializeCharacters()
    {
        Player player = FindFirstObjectByType<Player>();

        if (player)
        {
            StatusSystem playerStatusSystem = new StatusSystem();

            statusEventRouter?.Bind(playerStatusSystem);
            player.Initialize(playerStatusSystem);
        }
    }
}