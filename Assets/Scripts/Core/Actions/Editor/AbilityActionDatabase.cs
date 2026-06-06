using System;
using System.Collections.Generic;
using System.Linq;

public static class AbilityActionDatabase
{
    public static List<Type> GetAllActions()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && typeof(AbilityAction).IsAssignableFrom(t))
            .ToList();
    }
}