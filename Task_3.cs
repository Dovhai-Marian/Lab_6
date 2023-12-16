using System;
using System.Collections.Generic;

public class FunctionCache<TKey, TResult>
{
    private Dictionary<TKey, Tuple<TResult, DateTime>> cache = new Dictionary<TKey, Tuple<TResult, DateTime>>();
    private TimeSpan cacheDuration;

    public FunctionCache(TimeSpan cacheDuration)
    {
        this.cacheDuration = cacheDuration;
    }

    public TResult GetOrAdd(TKey key, Func<TKey, TResult> function)
    {
        if (cache.TryGetValue(key, out var cachedItem) && DateTime.Now - cachedItem.Item2 < cacheDuration)
        {
            return cachedItem.Item1;
        }

        TResult result = function(key);
        cache[key] = Tuple.Create(result, DateTime.Now);
        return result;
    }
}

class Program
{
    static void Main()
    {
        FunctionCache<string, int> cache = new FunctionCache<string, int>(TimeSpan.FromSeconds(10));
        int result = cache.GetOrAdd("key", key =>
        {
            Console.WriteLine("Calculating result...");
            return key.Length;
        });

        Console.WriteLine("Result: " + result);

        int cachedResult = cache.GetOrAdd("key", key => 0);
        Console.WriteLine("Cached Result: " + cachedResult);
    }
}
