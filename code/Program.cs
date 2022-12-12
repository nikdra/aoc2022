/// <summary>
/// Entry Program for each day of AoC
/// </summary>
public class Program 
{
    static void Main() 
    {   
        // instantiate solver code
        Solver sol = new Day12();

        // start the stopwatch
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();

        // part one
        sol.SolvePartOne();

        // part two
        sol.SolvePartTwo();

        // stop the stopwatch
        watch.Stop();

        // print execution time
        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds } ms");
    }
}
