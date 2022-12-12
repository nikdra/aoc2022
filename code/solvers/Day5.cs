using System.Text.RegularExpressions;
public class Day5 : Solver
{

    record instruction(int num, int from, int to);

    IEnumerable<instruction> inp;

    List<List<string>> stacks = new List<List<string>>()
    {   
        new List<string>(){"R","C","H"},
        new List<string>(){"F","S","L","H","J","B"},
        new List<string>(){"Q","T","J","H","D","M","R"},
        new List<string>(){"J","B","Z","H","R","G","S"},
        new List<string>(){"B","C","D","T","Z","F","P","R"},
        new List<string>(){"G","C","H","T"},
        new List<string>(){"L","W","P","B","Z","V","N","S"},
        new List<string>(){"C","G","Q","J","R"},
        new List<string>(){"S","F","P","H","R","T","D","L"}
    };

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day5 () : base() 
    {
        string pattern = @"move (\d+) from (\d+) to (\d+)";
        this.inp = File.ReadAllLines("../input/main/05")
            .Select(line => Regex.Match(line, pattern))
            .Select(match => new instruction(Int32.Parse(match.Groups[1].Value), Int32.Parse(match.Groups[2].Value)-1, Int32.Parse(match.Groups[3].Value)-1))
            .ToList();
    }

    private void moveCrates(instruction instruction) {
        this.stacks[instruction.to] = this.stacks[instruction.from].Take(instruction.num).Reverse().ToList().Concat(this.stacks[instruction.to]).ToList();
        this.stacks[instruction.from] = this.stacks[instruction.from].TakeLast(this.stacks[instruction.from].Count - instruction.num).ToList();
    }

    /// <summary>
    /// In how many assignment pairs does one range fully contain the other?
    /// </summary>
    public override void SolvePartOne()
    {
        this.inp.ToList().ForEach(line => moveCrates(line));
        string result = String.Join("", this.stacks.Select(stack => stack[0]));
        Console.WriteLine(result);
    }

    private void moveCrates9001(instruction instruction) {
        this.stacks[instruction.to] = this.stacks[instruction.from].Take(instruction.num).ToList().Concat(this.stacks[instruction.to]).ToList();
        this.stacks[instruction.from] = this.stacks[instruction.from].TakeLast(this.stacks[instruction.from].Count - instruction.num).ToList();
    }

    /// <summary>
    /// In how many assignment pairs do the ranges overlap?
    /// </summary>
    public override void SolvePartTwo()
    {
        this.stacks = new List<List<string>>()
        {   
            new List<string>(){"R","C","H"},
            new List<string>(){"F","S","L","H","J","B"},
            new List<string>(){"Q","T","J","H","D","M","R"},
            new List<string>(){"J","B","Z","H","R","G","S"},
            new List<string>(){"B","C","D","T","Z","F","P","R"},
            new List<string>(){"G","C","H","T"},
            new List<string>(){"L","W","P","B","Z","V","N","S"},
            new List<string>(){"C","G","Q","J","R"},
            new List<string>(){"S","F","P","H","R","T","D","L"}
        };
        this.inp.ToList().ForEach(line => moveCrates9001(line));
        string result = String.Join("", this.stacks.Select(stack => stack[0]));
        Console.WriteLine(result);
    }
}