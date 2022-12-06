namespace AdventOfCode;

public class Day6 : Day {
    public override int Number => 6;

    private static void Run(List<string> lines, int num) {
        var subroutine = new Subroutine();
        for (var i = 0; i < lines[0].Length; i++) {
            subroutine.AddCharacter(lines[0][i]);

            if (subroutine.CountLastDifferent(num)) {
                Console.WriteLine(i + 1);
                return;
            }
        }
    }

    protected override void Run_Part1(List<string> lines) => Run(lines, 4);

    protected override void Run_Part2(List<string> lines) => Run(lines, 14);

    public class Subroutine {
        private readonly List<char> Characters = new();
        private readonly HashSet<char> DistinctCharacterBuffer = new(4);

        public void AddCharacter(char c) {
            Characters.Add(c);
        }

        public bool CountLastDifferent(int num) {
            if (Characters.Count < num) {
                return false;
            }
            
            DistinctCharacterBuffer.Clear();
            for (var i = Characters.Count - num; i < Characters.Count; i++) {
                if (DistinctCharacterBuffer.Contains(Characters[i])) {
                    return false;
                }

                DistinctCharacterBuffer.Add(Characters[i]);
            }

            return true;
        }
    }
}