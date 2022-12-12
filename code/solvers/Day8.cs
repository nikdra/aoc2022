public class Day8:Solver {

    string[] grid;

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day8 () : base() 
    {
        this.grid = File.ReadAllLines("../input/main/08");
    }

    private bool visible(int y, int x) {
        if (x == 0 || y == 0 || x == this.grid[y].Length - 1 || y == this.grid.Length - 1) { // edge
            return true;
        }
        if (Enumerable.Range(0,y).All(up => grid[up][x] < grid[y][x])) {
            return true;
        }
        if (Enumerable.Range(y+1,grid.Length - y - 1).All(down => grid[down][x] < grid[y][x])) {
            return true;
        }
        if (Enumerable.Range(0,x).All(left => grid[y][left] < grid[y][x])) {
            return true;
        }
        if (Enumerable.Range(x+1,grid[0].Length - x - 1).All(right => grid[y][right] < grid[y][x])) {
            return true;
        }
        return false;
    }

    private int scenicScore(int y, int x) {
        int upScore = y - Enumerable.Range(0, y).Reverse().SkipWhile(up => grid[up][x] < grid[y][x]).FirstOrDefault(0);
        int downScore = Enumerable.Range(y + 1, grid.Length - y - 1).SkipWhile(down => grid[down][x] < grid[y][x]).FirstOrDefault(grid.Length-1) - y;
        int leftScore = x - Enumerable.Range(0, x).Reverse().SkipWhile(left => grid[y][left] < grid[y][x]).FirstOrDefault(0);
        int rightScore = Enumerable.Range(x + 1, grid[0].Length - x - 1).SkipWhile(right => grid[y][right] < grid[y][x]).FirstOrDefault(grid[y].Length-1) - x;
        return upScore * downScore * leftScore * rightScore;
    }

    /// <summary>
    /// Consider your map; how many trees are visible from outside the grid?
    /// </summary>
    public override void SolvePartOne()
    {
        int y = grid.Length;
        int x = grid[0].Length;

        int result = Enumerable.Range(0, y)
            .Select(y1 => Enumerable.Range(0, x)
                .Where(x1 => visible(y1, x1)))
            .SelectMany(x => x)
            .Count();

        Console.WriteLine(result);
    }

    /// <summary>
    /// Consider each tree on your map. What is the highest scenic score possible for any tree?
    /// </summary>
    public override void SolvePartTwo()
    {
        int y = grid.Length;
        int x = grid[0].Length;

        int result = Enumerable.Range(1, y-1)
            .Select(y1 => Enumerable.Range(1, x-1)
                .Select(x1 => scenicScore(y1, x1)))
            .SelectMany(x => x)
            .Max();

        Console.WriteLine(result);
    }

}