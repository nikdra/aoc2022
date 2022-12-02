public class Day2:Solver {

    /// <summary>
    /// An enumerable which represents the contents of a line in the input
    /// </summary>
    IEnumerable<string[]> inp;

    /// <summary>
    /// A record for the contents of a round
    /// </summary>
    /// <param name="opp"></param> Opponent choice (A,B,C)
    /// <param name="strat"></param> Player choice (X,Y,Z)
    record Round(string opp, string strat);

    /// <summary>
    /// A dict which translates the codes to one of (rock, paper, scissors)
    /// </summary>
    Dictionary<string, string> codeToChoice = new Dictionary<string, string>()
        {
             {"A", "rock"}
            ,{"B", "paper"}
            ,{"C", "scissors"}
            ,{"X", "rock"}
            ,{"Y", "paper"}
            ,{"Z", "scissors"}
        };

    /// <summary>
    /// The score for each hand
    /// </summary>
    Dictionary<string, long> selectionScore = new Dictionary<string, long>()
        {
             {"rock", 1}
            ,{"paper", 2}
            ,{"scissors", 3}
        };


    /// <summary>
    /// A dictionary which returns the hand that it beats for each hand
    /// </summary>
    Dictionary<string, string> beats = new Dictionary<string, string>()
        {
             {"rock", "scissors"}
            ,{"paper", "rock"}
            ,{"scissors", "paper"}
        };

    /// <summary>
    /// Calculate the score of a round
    /// </summary>
    /// <param name="round"></param>
    /// <returns></returns>
    private long outcomeScore (Round round) {
        string selection = round.strat;
        string opponent = round.opp;
        if (beats[selection] == opponent) {
            return 6 + selectionScore[selection];
        }
        if (selection == opponent) {
            return 3 + selectionScore[selection];
        }
        return 0 + selectionScore[selection];
    }

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day2 () : base() 
    {
        this.inp = System.IO.File.ReadAllLines("../input/main/02")
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries));
    }

    /// <summary>
    /// What would your total score be if everything goes exactly according to your strategy guide?
    /// </summary>
    public override void SolvePartOne()
    {
        long result = this.inp
            .Select(line => new Round(codeToChoice[line[0]], codeToChoice[line[1]]))
            .Select(round => outcomeScore(round))
            .Sum();
        Console.WriteLine(result);
    }

    /// <summary>
    /// Translate the player codes to one of (win, loss, draw)
    /// </summary>
    Dictionary<string, string> codeToOutcomeChoice = new Dictionary<string, string>()
        {
             {"X", "lose"}
            ,{"Y", "draw"}
            ,{"Z", "win"}
        };

    /// <summary>
    /// A dictionary which returns the hand that it is beaten by for each hand
    /// </summary>
    Dictionary<string, string> beatenBy = new Dictionary<string, string>()
        {
             {"rock", "paper"}
            ,{"paper", "scissors"}
            ,{"scissors", "rock"}
        };

    /// <summary>
    /// Get the appropriate hand for the desired outcome
    /// </summary>
    /// <param name="round"></param>
    /// <returns>One of (rock,paper,scissors)</returns>
    private string getSelectionFromRoundChoice (Round round) {
        string strategy = round.strat;
        string opponent = round.opp;
        if (strategy == "win") {
            return beatenBy[opponent];
        }
        if (strategy == "draw") {
            return opponent;
        }
        return beats[opponent];
    }

    /// <summary>
    /// Following the Elf's instructions for the second column, 
    /// what would your total score be if everything goes exactly according to your strategy guide?
    /// </summary>
    public override void SolvePartTwo()
    {
        long result = this.inp.Select(line => new Round(codeToChoice[line[0]], codeToOutcomeChoice[line[1]]))
            .Select(round => outcomeScore(round with { strat = getSelectionFromRoundChoice(round) }))
            .Sum();
        Console.WriteLine(result);
    }
}