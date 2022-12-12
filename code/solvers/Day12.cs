public class Day12:Solver {

    Dictionary<Square, char> grid;

    int xmax;

    int ymax;

    Square endPoint;

    record Square(int x, int y);

    private IEnumerable<Square> getNeighbors(Square square) {
        // 4-neighbors of square with eleveation at most 1 higher than current square
        if (square.x > 0 && this.grid[square with {x = square.x - 1}] <= this.grid[square] + 1) {
            yield return square with {x = square.x - 1};
        }
        if (square.y > 0 && this.grid[square with {y = square.y - 1}] <= this.grid[square] + 1) {
            yield return square with { y = square.y - 1 };
        }
        if (square.x < xmax - 1 && this.grid[square with {x = square.x + 1 }] <= this.grid[square] + 1) {
            yield return square with { x = square.x + 1 };
        }
        if (square.y < ymax - 1 && this.grid[square with {y = square.y + 1}] <= this.grid[square] + 1) {
            yield return square with { y = square.y + 1 };
        }
    }

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day12 () : base() 
    {
        // setup grid
        string[] inp = File.ReadAllLines("../input/main/12");

        this.xmax = inp.Length;

        this.ymax = inp[0].Length;

        this.grid = Enumerable.Range(0, xmax)
            .SelectMany(x => Enumerable.Range(0, ymax)
                .Select(y => new Square(x, y)))
            .ToDictionary(square => square, square => inp[square.x][square.y]);

        // assuming there is only one end point
        this.endPoint = this.grid
            .Where(kv => kv.Value == 'E')
            .Select(kv => kv.Key)
            .First();

        // set end point to highest altitude
        this.grid[endPoint] = 'z';

    }

    private int shortestPath(Square startingPoint) {
        // dijkstra, basically
        // wikipedia priority queue version
        Dictionary<Square, int> dist = new Dictionary<Square, int>();
        Dictionary<Square, Square> prev = new Dictionary<Square, Square>();
        dist[startingPoint] = 0;

        PriorityQueue<Square, int> Q = new PriorityQueue<Square, int>();

        Q.Enqueue(startingPoint, 0);

        foreach (KeyValuePair<Square,char> kv in this.grid) {
            if (kv.Key != startingPoint) {
                dist[kv.Key] = Int32.MaxValue;
            }
        }

        while (Q.Count > 0) {
            Square u = Q.Dequeue();
            if (u == endPoint) {
                return dist[u];
            }
            foreach (Square v in getNeighbors(u)) {
                int alt = dist[u] + 1;
                if (alt < dist[v]) {
                    dist[v] = alt;
                    prev[v] = u;
                    Q.Enqueue(v, alt);
                }
            }
        }
        return Int32.MaxValue;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void SolvePartOne()
    {
        // assuming there is only one starting point
        Square startingPoint = this.grid
            .Where(kv => kv.Value == 'S')
            .Select(kv => kv.Key)
            .First();

        // set starting point to lowest altitude
        this.grid[startingPoint] = 'a';
        int result = shortestPath(startingPoint);
        Console.WriteLine(result);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void SolvePartTwo()
    {
        IEnumerable<Square> startingPoints = this.grid
            .Where(kv => kv.Value == 'a')
            .Select(kv => kv.Key);

        int result = startingPoints
            .Select(sp => shortestPath(sp))
            .Min();

        Console.WriteLine(result);
    }
}