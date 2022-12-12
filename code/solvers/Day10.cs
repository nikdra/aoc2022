public class Day10:Solver {

    string[] inp;

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day10 () : base() 
    {
        this.inp = File.ReadAllLines("../input/main/10");
    }

    private IEnumerable<int> execute() {
        int X = 1;
        foreach (string instruction in this.inp) {
            if (instruction == "noop") {
                yield return X;
            }
            else { // add instruction
                yield return X;
                yield return X;
                X += Int32.Parse(instruction.Split(" ")[1]);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void SolvePartOne()
    {
        List<int> values = execute().ToList();
        int result = Enumerable.Range(0, values.Count)
            .Where(i => ((i - 20) % 40) == 0)
            .Select(i => values[i-1] * i) // cycle counting starts at 1...
            .Sum();
        Console.WriteLine(result);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void SolvePartTwo()
    {
        List<int> values = execute().ToList();
        string[][] image = new string[6][] {
            Enumerable.Repeat(" ", 40).ToArray(),
            Enumerable.Repeat(" ", 40).ToArray(),
            Enumerable.Repeat(" ", 40).ToArray(),
            Enumerable.Repeat(" ", 40).ToArray(),
            Enumerable.Repeat(" ", 40).ToArray(),
            Enumerable.Repeat(" ", 40).ToArray()
        };
        Enumerable.Range(0, 6)
            .SelectMany(h => Enumerable.Range(0, 40)
                .Where(w => Math.Abs(w - values[h * 40 + w]) <= 1)
                .Select(w => new { h = h, w = w }))
            .ToList()
            .ForEach(pos => image[pos.h][pos.w] = "#");

        image.Select(line => String.Join("", line))
            .ToList()
            .ForEach(line => Console.WriteLine(line));
    }
}