namespace AdventOfCode; 

public abstract class Day {
    private const string PATH = @"C:\Users\EmmetOT\RiderProjects\AdventOfCode\AdventOfCode\Inputs\";

    public abstract int Number { get; }

    protected string TEST_FILE => $"Day{Number}Test.txt";
    protected string REAL_FILE => $"Day{Number}Real.txt";
    
    protected static List<string> ProcessInput(string path) {
        List<string> output = new();
    
        foreach (string line in File.ReadLines(path)) {
            output.Add(line);
        }

        return output;
    }

    protected List<string> ProcessRealInput() => ProcessInput(PATH + REAL_FILE);
    protected List<string> ProcessTestInput() => ProcessInput(PATH + TEST_FILE);
    
    protected abstract void Run_Part1(List<string> lines);
    protected abstract void Run_Part2(List<string> lines);
    
    public void Run_Part1_Test() => Run_Part1(ProcessTestInput());
    public void Run_Part2_Test() => Run_Part2(ProcessTestInput());
    public void Run_Part1_Real() => Run_Part1(ProcessRealInput());
    public void Run_Part2_Real() => Run_Part2(ProcessRealInput());
}