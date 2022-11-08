public class Day1:Solver {

    /// <summary>
    /// The input variable idk
    /// </summary>
    string inp;

    public Day1 (string inputFile) : base(inputFile) 
    {
        this.inp = "Hello World";
    }

    public override void SolvePartOne()
    {
        Console.WriteLine(this.inp);
    }

    public override void SolvePartTwo()
    {
        Console.WriteLine(this.inp + " but part two");
    }
}