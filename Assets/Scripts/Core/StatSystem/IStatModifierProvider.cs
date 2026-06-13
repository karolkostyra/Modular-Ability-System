using System.Collections.Generic;

public interface IStatModifierProvider
{
    IEnumerable<StatModifier> GetModifiers(StatType stat);
}