public class Day11:Solver {

    List<Monkey> inp;

    int overallMod;

    record Monkey(Queue<long> items, Func<long,long> operation, int test, int monkeyTrue, int monkeyFalse);

    private Monkey parseMonkeyLines(string[] lines) {
        Queue<long> items = new Queue<long>(lines[1].Split(": ")[1].Split(", ").Select(its => long.Parse(its)));
        string op = lines[2].Split(": ")[1];
        Func<long, long> operation;
        // beware jank operation parsing
        if (op.Split(" ")[3] == "+") {
            operation = old => old + Int32.Parse(op.Split(" ")[4]);
        } else {
            if (op.Split(" ")[4] == "old") {
                operation = old => old * old;
            } else {
                operation = old => old * Int32.Parse(op.Split(" ")[4]);
            }
        }
        int test = Int32.Parse(lines[3].Split(" ").Last());
        int monkeyTrue = Int32.Parse(lines[4].Split(" ").Last());
        int monkeyFalse = Int32.Parse(lines[5].Split(" ").Last());
        return new Monkey(items, operation, test, monkeyTrue, monkeyFalse);
    }

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day11 () : base() 
    {
        this.inp = File.ReadAllText("../input/main/11")
            .Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(monkey => parseMonkeyLines(monkey.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)))
            .ToList();
        // calculate the overall mod
        overallMod = this.inp
            .Select(monkey => monkey.test)
            .Aggregate((a, b) => a * b);
    }

    private long solve(List<Monkey> monkeys, int rounds, bool divideByThree) {
        long[] activity = Enumerable.Repeat(0L, monkeys.Count).ToArray();
        for (int round = 0; round < rounds; round++) {
            for (int i = 0; i < monkeys.Count; i++) {
                Monkey monkey = monkeys[i];
                while (monkey.items.Count > 0)
                {
                    long item = monkey.items.Dequeue();
                    activity[i]++;
                    item = monkey.operation(item);
                    if (divideByThree) { // part 1
                        item = item / 3;
                    }
                    item = item % overallMod;
                    if (item % monkey.test == 0)
                    {
                        monkeys[monkey.monkeyTrue].items.Enqueue(item);
                    }
                    else
                    {
                        monkeys[monkey.monkeyFalse].items.Enqueue(item);
                    }
                }
            }
        }

        return activity.OrderByDescending(x => x).Take(2).Aggregate((a, b) => a * b);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void SolvePartOne()
    {
        long result = solve(this.inp, 20, true);
        Console.WriteLine(result);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void SolvePartTwo()
    {
        this.inp = File.ReadAllText("../input/main/11")
            .Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(monkey => parseMonkeyLines(monkey.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)))
            .ToList();
        long result = solve(this.inp, 10000, false);
        Console.WriteLine(result);
    }
}