public class Day9:Solver {

    record Movement(string direction, int steps);

    record Position(int x, int y);

    IEnumerable<Movement> inp;

    Dictionary<string, Position> vectors = new Dictionary<string, Position>() {
        {"R", new Position(1,0)},
        {"L", new Position(-1,0)},
        {"U", new Position(0,1)},
        {"D", new Position(0,-1)}
    };

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day9 () : base() 
    {
        this.inp = System.IO.File.ReadAllLines("../input/main/09")
            .Select(line => new Movement(line.Split(" ")[0], Int32.Parse(line.Split(" ")[1])));
    }

    private Position addPosition(Position pos1, Position pos2) {
        return new Position(pos1.x + pos2.x, pos1.y + pos2.y);
    }

    private Position updatePosition(Position head, Position tail) {
        if (Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1) {
            return tail;
        }
        if (head.x == tail.x) {
            return tail with { y = tail.y + (tail.y < head.y ? 1 : -1) };
        }
        if (head.y == tail.y) {
            return tail with { x = tail.x + (tail.x < head.x ? 1 : -1) };
        }
        return new Position( tail.x + (tail.x < head.x ? 1 : -1), tail.y + (tail.y < head.y ? 1 : -1) );
    }

    private HashSet<Position> moveRope(int ropeLength) {
        HashSet<Position> positions = new HashSet<Position>();
        Position[] rope = Enumerable.Range(0, ropeLength)
            .Select(i => new Position(0, 0))
            .ToArray();
        foreach (Movement movement in this.inp) {
            foreach (int i in Enumerable.Range(0, movement.steps)) {
                rope[0] = addPosition(rope[0], vectors[movement.direction]);
                foreach (int j in Enumerable.Range(1, ropeLength-1)){
                    rope[j] = updatePosition(rope[j-1], rope[j]);
                }
                positions.Add(rope[ropeLength-1]);
            }
        }
        return positions;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void SolvePartOne()
    {
        int result = moveRope(2).Count();
        Console.WriteLine(result);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void SolvePartTwo()
    {
        int result = moveRope(10).Count();
        Console.WriteLine(result);
    }
}