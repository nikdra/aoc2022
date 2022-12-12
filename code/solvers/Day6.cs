public class Day6:Solver {

    string inp;
    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day6 () : base() 
    {
        this.inp = File.ReadAllText("../input/main/06");
        //this.inp = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
    }

    /// <summary>
    /// In how many assignment pairs does one range fully contain the other?
    /// </summary>
    public override void SolvePartOne()
    {
        int i = 4;

        while (this.inp[(i-4)..i].ToHashSet().Count != 4) {
            i++;
        }
        Console.WriteLine(i);
    }

    /// <summary>
    /// In how many assignment pairs do the ranges overlap?
    /// </summary>
    public override void SolvePartTwo()
    {
        int i = 14;

        while (this.inp[(i-14)..i].ToHashSet().Count != 14) {
            i++;
        }
        Console.WriteLine(i);
    }
}