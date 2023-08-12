using System.Diagnostics;
using static System.Console;

Stopwatch watch = new();
Write("Press ENTER to Start");
ReadLine();
watch.Start();
int max = 45;

IEnumerable<int> nums = Enumerable.Range(1, max);
WriteLine($"Calculating Fibonacci sequence up to {max} - Please wait...");

int[] fiboNums = nums.AsParallel().Select(n => Fib(n)).OrderBy(num => num).ToArray();
watch.Stop();
WriteLine(watch.ElapsedMilliseconds);

Write("Results:");
foreach (int number in fiboNums) {
    Write($" {number}");
}

static int Fib(int term) =>
    term switch {
        1 => 0,
        2 => 1,
        _ => Fib(term - 1) + Fib(term - 2)
    };
