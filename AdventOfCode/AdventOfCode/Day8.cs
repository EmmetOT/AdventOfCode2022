namespace AdventOfCode; 

public class Day8 : Day {
    public override int Number => 8;
    
    protected override void Run_Part1(List<string> lines) {
        var trees = ParseTrees(lines);
        var visibleTreeCount = 0;
        
        for (var i = 0; i < lines.Count; i++) {
            for (var j = 0; j < lines[i].Length; j++) {
                if (CheckIsVisible(j, i, trees)) {
                    visibleTreeCount++;
                }
            }
        }
        
        Console.WriteLine(visibleTreeCount);
    }

    protected override void Run_Part2(List<string> lines) {
        Console.WriteLine(GetBestScenicScore(ParseTrees(lines)));
    }

    private int GetBestScenicScore(int[,] trees) {
        var bestScore = 0;
        for (var i = 0; i < trees.GetLength(0); i++) {
            for (var j = 0; j < trees.GetLength(1); j++) {
                var score = GetScenicScore(j, i, trees);
                if (score > bestScore) {
                    bestScore = score;
                }
            }
        }

        return bestScore;
    }
    
    private int GetScenicScore(int x, int y, int[,] trees) {
        var height = trees[y, x];

        var leftScore = 0;
        
        for (var i = x - 1; i >= 0; i--) {
            ++leftScore;
            if (trees[y, i] >= height) {
                break;
            }
        }
        
        var rightScore = 0;
        
        for (var i = x + 1; i < trees.GetLength(1); i++) {
            ++rightScore;
            if (trees[y, i] >= height) {
                break;
            }
        }
        
        var aboveScore = 0;
        
        for (var i = y + 1; i < trees.GetLength(0); i++) {
            ++aboveScore;
            if (trees[i, x] >= height) {
                break;
            }
        }
        
        var belowScore = 0;
        
        for (var i = y - 1; i >= 0; i--) {
            ++belowScore;
            if (trees[i, x] >= height) {
                break;
            }
        }

        return leftScore * rightScore * aboveScore * belowScore;
    }
    
    private bool CheckIsVisible(int x, int y, int[,] trees) {
        if (y == 0 || x == 0 || y == trees.GetLength(0) - 1 || x == trees.GetLength(1) - 1) {
            return true;
        }

        var height = trees[y, x];
        var visible = true;
        
        // check left row
        for (var i = 0; i < x; i++) {
            
            if (trees[y, i] >= height) {
                visible = false;
                break;
            }
        }

        if (visible) {
            return visible;
        }

        visible = true;
        
        // check right row
        for (var i = x + 1; i < trees.GetLength(1); i++) {
            if (trees[y, i] >= height) {
                visible = false;
                break;
            }
        }
        
        if (visible) {
            return visible;
        }

        visible = true;
        
        // check above column
        for (var i = 0; i < y; i++) {
            if (trees[i, x] >= height) {
                visible = false;
                break;
            }
        }

        if (visible) {
            return visible;
        }

        visible = true;
        
        // check below column
        for (var i = y + 1; i < trees.GetLength(0); i++) {
            if (trees[i, x] >= height) {
                visible = false;
                break;
            }
        }
        
        if (visible) {
            return visible;
        }

        return false;

    }

    private static int[,] ParseTrees(IList<string> lines) {
        var trees = new int[lines.Count, lines[0].Length];

        for (var i = 0; i < lines.Count; i++) {
            for (var j = 0; j < lines[i].Length; j++) {
                trees[i, j] = int.Parse(lines[i][j].ToString());
            }
        }

        return trees;
    }
 }