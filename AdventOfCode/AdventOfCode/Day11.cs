namespace AdventOfCode;

public class Day11 : Day {
    public override int Number => 11;

    protected override void Run_Part1(List<string> lines) {
        var monkeys = ParseMonkeys(lines);

        for (int round = 0; round < 20; round++) {
            for (var index = 0; index < monkeys.Count; index++) {
                var monkey = monkeys[index];
                if (monkey.Items.Count == 0) {
                    continue;
                }

                foreach (var item in monkey.Items) {
                    var transformed = monkey.ApplyOp(item);
                    transformed = (int)(transformed / 3f);
                    monkey.TimesInspected++;
                    var targetMonkey = monkey.GetTargetMonkeyIndex(transformed);
                    monkeys[targetMonkey].Items.Add(transformed);
                }

                monkey.Items.Clear();
            }
        }

        var inspectedTimes = new List<long>(monkeys.Count);
        for (int i = 0; i < monkeys.Count; i++) {
            inspectedTimes.Add(monkeys[i].TimesInspected);
        }

        inspectedTimes.Sort();
        
        Console.WriteLine((inspectedTimes[^1] * inspectedTimes[^2]));
    }

    protected override void Run_Part2(List<string> lines) {
        var monkeys = ParseMonkeys(lines);
        int mod = 1;

        for (int i = 0; i < monkeys.Count; i++) {
            mod *= monkeys[i].TestDiv;
        }

        for (int round = 0; round < 10000; round++) {
            for (var index = 0; index < monkeys.Count; index++) {
                var monkey = monkeys[index];
                if (monkey.Items.Count == 0) {
                    continue;
                }

                foreach (var item in monkey.Items) {
                    var transformed = monkey.ApplyOp(item);
                    monkey.TimesInspected++;
                    var targetMonkey = monkey.GetTargetMonkeyIndex(transformed);
                    transformed %= mod;
                    monkeys[targetMonkey].Items.Add(transformed);
                }

                monkey.Items.Clear();
            }
        }

        var inspectedTimes = new List<long>(monkeys.Count);
        for (int i = 0; i < monkeys.Count; i++) {
            inspectedTimes.Add(monkeys[i].TimesInspected);
        }

        inspectedTimes.Sort();
        
        Console.WriteLine((inspectedTimes[^1] * inspectedTimes[^2]));
    }

    private List<Monkey> ParseMonkeys(IList<string> lines) {
        var monkeys = new List<Monkey>();
        for (var i = 0; i < lines.Count; i++) {
            var line = lines[i];
            if (line.StartsWith("  Starting items: ")) {
                var monkey = new Monkey();
                var split = line.Split(' ');
                foreach (var splitString in split) {
                    if (long.TryParse(splitString.Trim(','), out var item)) {
                        monkey.Items.Add(item);
                    }
                }
                
                line = lines[++i];
                split = line.Split(' ');
                var op = split[6];
                var operand = split[7];
                monkey.SetOperation(op, operand);

                line = lines[++i];
                split = line.Split(' ');
                monkey.TestDiv = int.Parse(split[5]);

                line = lines[++i];
                split = line.Split(' ');
                monkey.TestTrueTargetMonkey = int.Parse(split[9]);

                line = lines[++i];
                split = line.Split(' ');
                monkey.TestFalseTargetMonkey = int.Parse(split[9]);

                monkeys.Add(monkey);
            }
        }

        return monkeys;
    }

    private class Monkey {
        public List<long> Items = new();

        public enum OpType {
            Add,
            Subtract,
            Multiply,
            Divide
        }

        public OpType Op;

        public bool UseOld = false;
        public long Operand = 0;
        public int TestDiv = 1;
        public int TestTrueTargetMonkey;
        public int TestFalseTargetMonkey;
        public long TimesInspected = 0;

        public string GetItemString() {
            return string.Join(", ", Items);
        }
        
        public int GetTargetMonkeyIndex(long input) {
            return (input % TestDiv) == 0 ? TestTrueTargetMonkey : TestFalseTargetMonkey;
        }
        
        public long ApplyOp(long input) {
            var operand = UseOld ? input : Operand;
            switch (Op) {
                case OpType.Add:
                    return input + operand;
                case OpType.Multiply:
                    return input * operand;
                case OpType.Subtract:
                    return input - operand;
                case OpType.Divide:
                    return input / operand;
                default:
                    throw new Exception();
            }
        }
        
        public void SetOperation(string op, string operand) {
            Op = op switch {
                    "+" => OpType.Add,
                    "-" => OpType.Subtract,
                    "*" => OpType.Multiply,
                    "/" => OpType.Divide,
            };

            if (operand.Equals("old")) {
                UseOld = true;
            } else {
                Operand = long.Parse(operand);
            }
        }
    }
}