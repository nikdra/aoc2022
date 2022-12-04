public class Day4:Solver {

    /// <summary>
    /// An enumerable which represents each pair of assignments
    /// </summary>
    IEnumerable<Pair> inp;

    /// <summary>
    /// A record for a clearing assignment
    /// </summary>
    /// <param name="min">The beginning of the section assigment range</param>
    /// <param name="max">The end of the section assigment range</param>
    record Assignment(int min, int max);

    /// <summary>
    /// A record for each pair of assignments (a line in the input)
    /// </summary>
    /// <param name="a1">The first assignment</param>
    /// <param name="a2">The second assignment</param>
    record Pair(Assignment a1, Assignment a2);

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day4 () : base() 
    {
        this.inp = File.ReadAllLines("../input/main/04")
            .Select(line => line.Split(",", StringSplitOptions.RemoveEmptyEntries))
            .Select(pair => new Pair(new Assignment(Int32.Parse(pair[0].Split("-")[0]), Int32.Parse(pair[0].Split("-")[1]))
                , new Assignment(Int32.Parse(pair[1].Split("-")[0]), Int32.Parse(pair[1].Split("-")[1]))));
    }

    /// <summary>
    /// In how many assignment pairs does one range fully contain the other?
    /// </summary>
    public override void SolvePartOne()
    {
        int result = this.inp
            .Select(pair => (pair.a1.min <= pair.a2.min && pair.a1.max >= pair.a2.max) || (pair.a1.min >= pair.a2.min && pair.a1.max <= pair.a2.max) ? 1 : 0)
            .Sum();

        Console.WriteLine(result);
    }

    /// <summary>
    /// In how many assignment pairs do the ranges overlap?
    /// </summary>
    public override void SolvePartTwo()
    {
        int result = this.inp 
            .Select(pair => 
                   (pair.a1.min <= pair.a2.min && pair.a1.max >= pair.a2.max) // a1 contains a2
                || (pair.a1.min >= pair.a2.min && pair.a1.max <= pair.a2.max) // a2 contains a1
                || (pair.a1.min <= pair.a2.min && pair.a2.min <= pair.a1.max) // a1 overlaps a2 "left"
                || (pair.a2.min <= pair.a1.min && pair.a1.min <= pair.a2.max) ? 1 : 0) // a2 overlaps a1 "left"
            .Sum();

        Console.WriteLine(result);
    }
}