using System;
using System.Collections.Generic;
using System.Linq;

public static class StatusConditionDatabase
{
    public static List<Type> GetAllConditions()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && typeof(StatusCondition).IsAssignableFrom(t))
            .ToList();
    }
}