namespace AdventOfCode;

public class Day10 : Day {
    public override int Number => 10;

    protected override void Run_Part1(List<string> lines) {
        var cycle = 0;
        var x = 1;
        var sum = 0;

        foreach (var line in lines) {
            var split = line.Split(" ");
            IncrementCycle();
            if (!split[0].Equals("noop")) {
                IncrementCycle();
                x += int.Parse(split[1]);
            }
        }

        Console.WriteLine(sum);

        void IncrementCycle() {
            cycle++;
            if (cycle == 20 || (cycle - 20) % 40 == 0) {
                sum += (x * cycle);
            }
        }
    }

    protected override void Run_Part2(List<string> lines) {
        var cycle = 0;
        var x = 1;
        var crt = new bool[6, 40];

        foreach (var line in lines) {
            var split = line.Split(" ");
            AddPixel();
            cycle++;

            if (!split[0].Equals("noop")) {
                AddPixel();
                cycle++;
                x += int.Parse(split[1]);
            }
        }

        Draw();

        void AddPixel() {
            var row = cycle / 40;
            var column = cycle % 40;
            if (column == (x - 1) || column == x || column == (x + 1)) {
                crt[row, column] = true;
            }
        }

        void Draw() {
            for (int row = 0; row < crt.GetLength(0); row++) {
                for (int column = 0; column < crt.GetLength(1); column++) {
                    Console.Write(crt[row, column] ? "#" : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}