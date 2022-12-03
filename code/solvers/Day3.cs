public class Day3:Solver {

    /// <summary>
    /// An array of strings that represent the elves' rucksacks
    /// </summary>
    string[] inp;

    /// <summary>
    /// Get the value of an item represented by a character
    /// Lowercase item types a through z have priorities 1 through 26.
    /// Uppercase item types A through Z have priorities 27 through 52.
    /// </summary>
    /// <param name="c"></param>
    /// <returns>Item value</returns>
    private int getItemValue(char c) {
        if (Char.IsUpper(c)) {
            return (int)c - 38;  // ASCII for Z = 90, therefore -38
        }
        return (int)c - 96;  // ASCII for a = 97, therefore -96
    }

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day3 () : base() 
    {
        this.inp = System.IO.File.ReadAllLines("../input/main/03");
    }

    /// <summary>
    /// Find the item type that appears in both compartments of each rucksack. 
    /// What is the sum of the priorities of those item types?
    /// </summary>
    public override void SolvePartOne()
    {
        int result = this.inp
            // split each backpack into compartments
            .Select(line => new string[] { line[0..(line.Length / 2)], line[(line.Length / 2)..line.Length] })
            // get the intersecting element (guaranteed one)
            .Select(rucksack => rucksack[0].Intersect(rucksack[1]).First())
            // get the value of that item
            .Select(commonElement => getItemValue(commonElement))
            // sum up
            .Sum();

        Console.WriteLine(result);
    }

    /// <summary>
    /// Find the item type that corresponds to the badges of each three-Elf group. 
    /// What is the sum of the priorities of those item types?
    /// </summary>
    public override void SolvePartTwo()
    {
        int result = Enumerable.Range(0, this.inp.Length / 3)  // guaranteed n*3 elements in input
            // get the intersecting element of three consecutive backpacks (guaranteed one)
            .Select(ind => this.inp[ind * 3].Intersect(this.inp[ind * 3 + 1]).Intersect(this.inp[ind * 3 + 2]).First())
            // Get the value of that item
            .Select(badge => getItemValue(badge))
            // Sum up
            .Sum();
        Console.WriteLine(result);
    }
}