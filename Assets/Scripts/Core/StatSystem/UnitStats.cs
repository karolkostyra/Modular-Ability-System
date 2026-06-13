using static UnityEngine.Rendering.DebugUI;

public class UnitStats
{
    private readonly UnitBaseStats baseStats;
    private readonly StatusSystem statusSystem;

    public UnitStats(UnitBaseStats baseStats, StatusSystem statusSystem)
    {
        this.baseStats = baseStats;
        this.statusSystem = statusSystem;
    }

    public float GetStat(StatType stat) //TODO: implement GetStat
    {
        return -1;
    }

    public float GetBaseStat(StatType stat)
    {
        return baseStats.Get(stat);
    }
}