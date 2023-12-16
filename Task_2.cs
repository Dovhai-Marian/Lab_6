using System;
using System.Collections.Generic;

public class Repository<T>
{
    public delegate bool Criteria(T item);

    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public List<T> Find(Criteria criteria)
    {
        return items.FindAll(criteria);
    }
}

class Program
{
    static void Main()
    {
        Repository<int> intRepository = new Repository<int>();
        intRepository.Add(1);
        intRepository.Add(2);
        intRepository.Add(3);

        List<int> result = intRepository.Find(item => item > 1);
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
    }
}
