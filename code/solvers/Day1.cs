public class Day1:Solver {

    /// <summary>
    /// An enumerable which represents the items of each elf
    /// </summary>
    IEnumerable<IEnumerable<Int32>> inp;

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day1 () : base() 
    {
        this.inp = System.IO.File.ReadAllText("../input/main/01")
            .Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(elf => elf.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(cal => Int32.Parse(cal)
                )
            );
    }

    /// <summary>
    /// Find the Elf carrying the most Calories. 
    /// How many total Calories is that Elf carrying?
    /// </summary>
    public override void SolvePartOne()
    {
        int result = this.inp.Select(items => items.Sum())
            .Max();
        Console.WriteLine(result);
    }

    /// <summary>
    /// Find the top three Elves carrying the most Calories. 
    /// How many Calories are those Elves carrying in total?
    /// </summary>
    public override void SolvePartTwo()
    {
        int result = this.inp.Select(items => items.Sum())
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
        Console.WriteLine(result);
    }
}