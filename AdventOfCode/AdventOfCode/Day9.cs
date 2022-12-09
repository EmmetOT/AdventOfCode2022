namespace AdventOfCode;

public class Day9 : Day {
    public override int Number => 9;

    private const int Grid_X = 1000;
    private const int Grid_Y = 1000;

    protected override void Run_Part1(List<string> lines) {
        var tail = new Position { X = Grid_X / 2, Y = Grid_Y / 2 };
        var head = new Position { X = Grid_X / 2, Y = Grid_Y / 2 };
        var grid = GenerateGrid(Grid_X, Grid_Y);

        foreach (var line in lines) {
            var split = line.Split(' ');
            var dir = split[0];
            var speed = int.Parse(split[1]);

            while (speed > 0) {
                switch (dir) {
                    case "U":
                        head += (0, -1);
                        break;
                    case "D":
                        head += (0, 1);
                        break;
                    case "L":
                        head += (-1, 0);
                        break;
                    case "R":
                        head += (1, 0);
                        break;
                }

                if (head.Distance(tail) > 1) {
                    tail = UpdateTail(tail, head);
                }

                grid[tail.X, tail.Y]++;

                speed--;
            }
        }

        Console.WriteLine(CountAllAtLeastOne(grid));
    }

    protected override void Run_Part2(List<string> lines) {
        var positions = new Position[10];
        for (var i = 0; i < positions.Length; i++) {
            positions[i] = new Position { X = Grid_X / 2, Y = Grid_Y / 2 };
        }

        var grid = GenerateGrid(Grid_X, Grid_Y);

        foreach (var line in lines) {
            var split = line.Split(' ');
            var dir = split[0];
            var speed = int.Parse(split[1]);

            while (speed > 0) {
                switch (dir) {
                    case "U":
                        positions[0] += (0, -1);
                        break;
                    case "D":
                        positions[0] += (0, 1);
                        break;
                    case "L":
                        positions[0] += (-1, 0);
                        break;
                    case "R":
                        positions[0] += (1, 0);
                        break;
                }

                for (var i = 1; i < positions.Length; i++) {
                    if (positions[i - 1].Distance(positions[i]) > 1) {
                        positions[i] = UpdateTail(positions[i], positions[i - 1]);
                    }
                }

                grid[positions[9].X, positions[9].Y]++;

                speed--;
            }
        }

        Console.WriteLine(CountAllAtLeastOne(grid));
    }

    private int CountAllAtLeastOne(int[,] grid) {
        int sum = 0;
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                if (grid[i, j] > 0)
                    sum++;
            }
        }

        return sum;
    }

    private Position UpdateTail(Position currentTail, Position head) {
        return new Position { X = currentTail.X + Math.Sign(head.X - currentTail.X), Y = currentTail.Y + Math.Sign(head.Y - currentTail.Y) };
    }

    private int[,] GenerateGrid(int width, int height) {
        var grid = new int[height, width];
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                grid[y, x] = 0;
            }
        }

        return grid;
    }

    private void PrintGrid(int[,] grid, Position head, Position tail) {
        for (var y = 0; y < grid.GetLength(1); y++) {
            for (var x = 0; x < grid.GetLength(0); x++) {
                var output = grid[x, y] > 0 ? "#" : ".";
                if (x == tail.X && y == tail.Y) {
                    output = "T";
                }

                if (x == head.X && y == head.Y) {
                    output = "H";
                }

                Console.Write(output);
            }

            Console.WriteLine();
        }
    }

    private struct Position {
        public int X { get; set; }
        public int Y { get; set; }

        public void Add(int x, int y) {
            X += x;
            Y += y;
        }

        public static Position operator +(Position pos, (int x, int y) other) {
            return new Position { X = pos.X + other.x, Y = pos.Y + other.y };
        }

        public int Distance(Position other) {
            // compute Chebyshev distance
            return Math.Max(Math.Abs(X - other.X), Math.Abs(Y - other.Y));
        }
    }
}