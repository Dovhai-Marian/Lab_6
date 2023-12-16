using System;

public class Calculator<T>
{
    public delegate T ArithmeticOperation(T x, T y);

    public ArithmeticOperation Add { get; } = (x, y) => (dynamic)x + y;
    public ArithmeticOperation Subtract { get; } = (x, y) => (dynamic)x - y;
    public ArithmeticOperation Multiply { get; } = (x, y) => (dynamic)x * y;
    public ArithmeticOperation Divide { get; } = (x, y) => (dynamic)x / y;

    public T PerformOperation(ArithmeticOperation operation, T x, T y)
    {
        return operation(x, y);
    }
}

class Program
{
    static void Main()
    {
        Calculator<int> intCalculator = new Calculator<int>();
        int result = intCalculator.PerformOperation(intCalculator.Add, 5, 3);
        Console.WriteLine(result);

        Calculator<double> doubleCalculator = new Calculator<double>();
        double doubleResult = doubleCalculator.PerformOperation(doubleCalculator.Divide, 10.0, 2.0);
        Console.WriteLine(doubleResult);
    }
}
