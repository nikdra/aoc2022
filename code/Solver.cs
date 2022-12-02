/// <summary>
/// Abstract Class for the day-to-day solution code to AoC
/// </summary>
public abstract class Solver
{
    /// <summary>
    /// Instantiate the Solver class
    /// This function should also read the input from the file and store it somehow
    /// </summary>
    public Solver () {}

    /// <summary>
    /// Function that solves the first part of a puzzle and prints it to console
    /// </summary>
    public virtual void SolvePartOne() {}

    /// <summary>
    /// Function that solves the second part of a puzzle and prints it to console
    /// </summary>
    public virtual void SolvePartTwo() {}
}