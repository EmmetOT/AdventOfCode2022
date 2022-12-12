namespace AdventOfCode; 

public class Day12 : Day {
    public override int Number => 12;
    
    protected override void Run_Part1(List<string> lines) {
        var world = new World(lines[0].Length, lines.Count);
        
        for (var y = 0; y < lines.Count; y++) {
            for (var x = 0; x < lines[y].Length; x++) {
                world.SetElevation(x, y, lines[y][x]);
            }
        }
        
        Console.WriteLine(GetPathLength(world, world.startPosition.x, world.startPosition.y));
        
    }

    protected override void Run_Part2(List<string> lines) {
        var world = new World(lines[0].Length, lines.Count);
        
        for (var y = 0; y < lines.Count; y++) {
            for (var x = 0; x < lines[y].Length; x++) {
                world.SetElevation(x, y, lines[y][x]);
            }
        }

        var shortestPathLength = int.MaxValue;
        
        for (var y = 0; y < world.height; y++) {
            for (var x = 0; x < world.width; x++) {
                if (world.GetElevation(x, y) == 0) {
                    var pathLength = GetPathLength(world, x, y);

                    if (pathLength != -1 && pathLength < shortestPathLength) {
                        shortestPathLength = pathLength;
                    }
                }
            }
        }
        
        Console.WriteLine(shortestPathLength);
    }
    
    private int GetPathLength(World world, int startX, int startY) {
        var directions = new List<(int x, int y)>(4);
        var visited = new HashSet<(int x, int y)>();
        var queue = new Queue<(int x, int y, int pathLength)>();
        queue.Enqueue((startX, startY, 0));

        while (queue.Count > 0) {
            var (x, y, pathLength) = queue.Dequeue();
            if (visited.Contains((x, y))) {
                continue;
            }
            
            visited.Add((x, y));

            if (x == world.targetPosition.x && y == world.targetPosition.y) {
                return pathLength;
            }
            
            world.GetPossibleDirections(x, y, directions);

            foreach (var dir in directions) {
                queue.Enqueue((dir.x, dir.y, pathLength + 1));
            }
        }

        return -1;
    }

    public class World {
        private readonly int[,] grid;
        public (int x, int y) startPosition;
        public (int x, int y) targetPosition;
        public int width => grid.GetLength(0);
        public int height => grid.GetLength(1);
        
        public World(int width, int height) {
            grid = new int[width, height];
        }

        public int GetElevation(int x, int y) {
            return grid[x, y];
        }

        public void GetPossibleDirections(int x, int y, List<(int x, int y)> directions) {
            directions.Clear();
            
            var currentElevation = grid[x, y];
            
            // check above
            if (y > 0 && grid[x, y - 1] <= currentElevation + 1) {
                directions.Add((x, y - 1));
            }
            
            // check below
            if (y < grid.GetLength(1) - 1 && grid[x, y + 1] <= currentElevation + 1) {
                directions.Add((x, y + 1));
            }
            
            // check left
            if (x > 0 && grid[x - 1, y] <= currentElevation + 1) {
                directions.Add((x - 1, y));
            }
            
            // check right
            if (x < grid.GetLength(0) - 1 && grid[x + 1, y] <= currentElevation + 1) {
                directions.Add((x + 1, y));
            }
        }

        public void SetElevation(int x, int y, char c) {
            if (c == 'E') {
                targetPosition = (x, y);
                SetElevation(x, y, 'z');
            } else if (c == 'S') {
                startPosition = (x, y);
                SetElevation(x, y, 'a');
            } else {
                grid[x, y] = c - 'a';
            }
        }
    }
}