using System;
using System.Collections.Generic;

public class TaskScheduler<TTask, TPriority>
{
    public delegate void TaskExecution(TTask task);

    private PriorityQueue<TTask, TPriority> taskQueue = new PriorityQueue<TTask, TPriority>();
    private Func<TTask, TPriority> priorityFunc;

    public TaskScheduler(Func<TTask, TPriority> priorityFunc)
    {
        this.priorityFunc = priorityFunc;
    }

    public void AddTask(TTask task)
    {
        taskQueue.Enqueue(task, priorityFunc(task));
    }

    public void ExecuteNext(TaskExecution executeTask)
    {
        if (taskQueue.Count > 0)
        {
            TTask nextTask = taskQueue.Dequeue();
            executeTask(nextTask);
        }
        else
        {
            Console.WriteLine("No tasks in the queue.");
        }
    }
}

class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
{
    private List<Tuple<TElement, TPriority>> elements = new List<Tuple<TElement, TPriority>>();

    public int Count => elements.Count;

    public void Enqueue(TElement element, TPriority priority)
    {
        elements.Add(Tuple.Create(element, priority));
        elements.Sort((x, y) => x.Item2.CompareTo(y.Item2));
    }

    public TElement Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        TElement element = elements[0].Item1;
        elements.RemoveAt(0);
        return element;
    }
}

class Program
{
    static void Main()
    {
        TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>(task => task.Length);

        scheduler.AddTask("Task 1");
        scheduler.AddTask("Task 2");
        scheduler.AddTask("Task 3");

        scheduler.ExecuteNext(task => Console.WriteLine("Executing task: " + task));
    }
}
