namespace AdventOfCode;

public class Day3 : Day {
    public override int Number => 3;

    protected override void Run_Part1(List<string> input) {
        int sum = 0;
        foreach (string line in input) {
            Rucksack rucksack = new Rucksack(line);
            sum += rucksack.commonItemPriority;
        }

        Console.WriteLine(sum);
    }

    protected override void Run_Part2(List<string> input) {
        int sum = 0;
        for (int i = 0; i < input.Count; i += 3) {
            Rucksack one = new Rucksack(input[i]);
            Rucksack two = new Rucksack(input[i + 1]);
            Rucksack three = new Rucksack(input[i + 2]);

            char common = Rucksack.FindCommonItem(one, two, three);
            sum += Rucksack.GetPriority(common);
            Console.WriteLine(common);
        }

        Console.WriteLine(sum);
    }

    public class Rucksack {
        public string full = string.Empty;
        public string firstCompartment = string.Empty;
        public string secondCompartment = string.Empty;
        public char commonItem = ' ';
        public int commonItemPriority => GetPriority(commonItem);

        public Rucksack(string itemString) {
            full = itemString;
            HashSet<char> itemsInFirstComparment = new();

            for (int i = 0; i < itemString.Length; i++) {
                if (i < itemString.Length / 2) {
                    firstCompartment += itemString[i];
                    itemsInFirstComparment.Add(itemString[i]);
                } else {
                    secondCompartment += itemString[i];
                }
            }

            for (int i = 0; i < secondCompartment.Length; i++) {
                if (itemsInFirstComparment.Contains(secondCompartment[i])) {
                    commonItem = secondCompartment[i];
                    break;
                }
            }
        }

        public static int GetPriority(char item) {
            int i = (int)item;

            if (i >= 65 && i <= 90)
                return i - 38;

            return i - 96;
        }

        public static char FindCommonItem(params Rucksack[] rucksacks) {
            HashSet<char> itemsInCommon = new(rucksacks[0].full);

            for (int i = 1; i < rucksacks.Length; i++) {
                HashSet<char> items = new(rucksacks[i].full);

                itemsInCommon = itemsInCommon.Intersect(items).ToHashSet();
            }

            if (itemsInCommon.Count != 1)
                Console.WriteLine(rucksacks.Length + " rucksacks were found to contain more than one item in common");

            return itemsInCommon.First();
        }
    }
}