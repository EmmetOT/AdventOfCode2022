using System.Diagnostics;

namespace AdventOfCode; 

public class Day4 : Day {
    public override int Number => 4;
    
    protected override void Run_Part1(List<string> input) {
        int completeOverlapCount = 0;
        
        foreach (string line in input) {
            AreaPair pair = new AreaPair(line);

            if (pair.HasCompleteOverlap()) {
                completeOverlapCount++;
            }
        }
        
        Console.WriteLine(completeOverlapCount);
    }

    protected override void Run_Part2(List<string> input) {
        int partialOverlapCount = 0;
        
        foreach (string line in input) {
            AreaPair pair = new AreaPair(line);

            if (pair.HasPartialOverlap()) {
                partialOverlapCount++;
            }
        }
        
        Console.WriteLine(partialOverlapCount);
    }

    public struct Area {
        public int Start;
        public int End;

        public Area(string areaString) {
            string[] split = areaString.Split('-');
            Start = int.Parse(split[0]);
            End = int.Parse(split[1]);
        }

        public bool Overlaps(Area other) {
            return Start <= other.Start && End >= other.Start;
        }
        
        public bool Contains(Area other) {
            return Start <= other.Start && End >= other.End;
        }
    }

    public struct AreaPair {
        public Area One;
        public Area Two;

        public AreaPair(string areaPairString) {
            string[] split = areaPairString.Split(',');
            One = new Area(split[0]);
            Two = new Area(split[1]);
        }

        public bool HasPartialOverlap() {
            return One.Overlaps(Two) || Two.Overlaps(One);
        }

        public bool HasCompleteOverlap() {
            return One.Contains(Two) || Two.Contains(One);
        }
    }
}