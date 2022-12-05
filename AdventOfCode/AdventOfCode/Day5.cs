namespace AdventOfCode; 

public class Day5 : Day {
    public override int Number => 5;
    protected override void Run_Part1(List<string> lines) {
        var (stacks, orders) = ConvertStrings(lines);
        
        // execute orders
        for (var i = 0; i < orders.Count; i++) {
            var order = orders[i];
            for (var j = 0; j < order.Quantity; j++) {
                stacks[order.To].AddLast(stacks[order.From].PopLast());
            }
        }

        foreach (var stack in stacks) {
            Console.Write(stack.Last.Value);
        }
        
        Console.WriteLine();
    }

    protected override void Run_Part2(List<string> lines) {
        var (stacks, orders) = ConvertStrings(lines);
        
        LinkedList<char> crane = new();

        // execute orders
        for (var i = 0; i < orders.Count; i++) {
            var order = orders[i];
            crane.Clear();
            for (var j = 0; j < order.Quantity; j++) {
                crane.AddLast(stacks[order.From].PopLast());
            }

            while (crane.Count > 0) {
                stacks[order.To].AddLast(crane.PopLast());
            }
        }

        foreach (var stack in stacks) {
            Console.Write(stack.Last.Value);
        }
        
        Console.WriteLine();
    }

    private static void PrintStacks(List<LinkedList<char>> stacks) {
        List<LinkedList<char>> copy = new(stacks.Count);
        foreach (var stack in stacks) {
            copy.Add(new(stack));
        }
        
        // get max stack height
        int maxStackHeight = 0;
        foreach (var stack in copy) {
            maxStackHeight = Math.Max(maxStackHeight, stack.Count);
        }
        
        while (maxStackHeight > 0) {
            int j = 0;
            foreach (var stack in copy) {
                if (stack.Count < maxStackHeight) {
                    Console.Write(" ");
                } else {
                    Console.Write(stack.PopLast());
                }
                Console.Write("|");
            }

            maxStackHeight--;
            Console.WriteLine();
        }
    }
    
    private static (List<LinkedList<char>> stacks, List<Order> orders) ConvertStrings(List<string> input) {
        List<LinkedList<char>> stacks = new();
        List<Order> orders = new();
        var isAddingStacks = true;
        foreach (var line in input) {
            isAddingStacks &= !line.IsNullOrEmpty();
            if (isAddingStacks) {
                for (var i = 1; i < line.Length; i += 4) {
                    var c = line[i];
                    if (int.TryParse(c.ToString(), out _)) {
                        continue;
                    }
                    var stackIndex = (i - 1) / 4;
                    while (stacks.Count < stackIndex + 1) { 
                        stacks.Add(new LinkedList<char>());
                    }
                    if (c != ' ') {
                        stacks[stackIndex].AddFirst(c);
                    }
                }
            } else if (!line.IsNullOrEmpty()) {
                var splitLine = line.Split(' ');
                orders.Add(new Order { Quantity = int.Parse(splitLine[1]), From = int.Parse(splitLine[3]) - 1, To = int.Parse(splitLine[5]) - 1 });
            }
        }

        return (stacks, orders);
    }
    
    private struct Order {
        public int From;
        public int To;
        public int Quantity;
    }
}