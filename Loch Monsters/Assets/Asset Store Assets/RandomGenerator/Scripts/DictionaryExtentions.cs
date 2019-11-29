using System;
using System.Collections.Generic;

namespace RandomGenerator.Scripts
{
    public static class DictionaryExtentions
    {
        public static void AddOrUpdate<TK, TV>(this Dictionary<TK, TV> dict, TK key, TV value, Func<TV, TV> update)
        {
            TV existing;
            if (!dict.TryGetValue(key, out existing))
            {
                dict.Add(key, value);
            }
            else
            {
                dict[key] = update(existing);
            }
        }
    }
}