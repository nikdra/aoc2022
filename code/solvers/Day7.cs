public class Day7:Solver {

    List<string> directories = new List<string>();

    Dictionary<string, int> files = new Dictionary<string, int>();

    /// <summary>
    /// Initialize the solver by supplying the path to the input
    /// </summary>
    public Day7 () : base() 
    {
        string[] terminal = File.ReadAllText("../input/main/07").Split("$ ");
        string currentDirectory = "/";
        foreach (string str in terminal[1..]) {
            string[] instr = str.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            if (instr[0] == "cd /") {
                directories.Add("/");
            }
            else if (instr[0] == "cd ..") {
                currentDirectory = String.Join("/", currentDirectory.Split('/')[0..^2])+ "/";
            }
            else if (instr[0][0..2] == "cd") {
                currentDirectory += instr[0][3..] + "/";
            }
            else { // ls
                foreach (string ln in instr[1..]) {
                    if (ln[0..3] == "dir") { // directory
                        directories.Add(currentDirectory + ln[4..] + "/");
                    } else { // file
                        int size = Int32.Parse(ln.Split(' ')[0]);
                        string name = currentDirectory + ln.Split(' ')[1];
                        files.Add(name, size);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Find all of the directories with a total size of at most 100000. 
    /// What is the sum of the total sizes of those directories?
    /// </summary>
    public override void SolvePartOne()
    {
        int result = this.directories
            .Select(directory => this.files.Keys
                .Select(key => key.StartsWith(directory) ? this.files[key] : 0))
            .Select(sizes => sizes.Sum())
            .Where(dirSize => dirSize <= 100000)
            .Sum();

        Console.WriteLine(result);

    }

    /// <summary>
    /// Find the smallest directory that, if deleted, would free up enough space on the filesystem to run the update. 
    /// What is the total size of that directory?
    /// </summary>
    public override void SolvePartTwo()
    {
        IEnumerable<int> dirSizes = this.directories
            .Select(directory => this.files.Keys
                .Select(key => key.StartsWith(directory) ? this.files[key] : 0))
            .Select(sizes => sizes.Sum());

        int rootSize = dirSizes.Max();
        int spaceToFree = 30000000 - (70000000 - rootSize); // assuming there isn't enough disk space for the update

        int result = dirSizes
            .Where(size => size >= spaceToFree)
            .Min();

        Console.WriteLine(result);
    }
}