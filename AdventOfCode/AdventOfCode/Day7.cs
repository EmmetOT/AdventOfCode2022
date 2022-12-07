namespace AdventOfCode;

public class Day7 : Day {
    public override int Number => 7;

    private const int DiskSpace = 70000000;
    private const int RequiredTotalSpace = 30000000;

    protected override void Run_Part1(List<string> lines) {
        var root = BuildFileSystem(lines);
        root.Calc();
    }

    protected override void Run_Part2(List<string> lines) {
        var root = BuildFileSystem(lines);
        var currentFreeSpace = DiskSpace - root.Size;
        var smallest = root.FindSmallestChildWithAtLeastSpace(30000000 - currentFreeSpace);
        
         if (smallest != null) {
             Console.WriteLine(smallest.Size);
         }
    }

    private Directory BuildFileSystem(IEnumerable<string> lines) {
        var root = new Directory { Name = "root" };
        var current = root;
        var queue = new Queue<string>(lines);
        while (queue.Count > 0) {
            var line = queue.Dequeue();
            var splitLine = line.Split(' ');

            if (!splitLine[0].Equals("$")) {
                continue;
            }

            switch (splitLine[1]) {
                case "cd" when splitLine[2].Equals("/"):
                    current = root;
                    break;
                case "cd" when splitLine[2].Equals(".."):
                    current = current.Parent;
                    break;
                case "cd": {
                    foreach (var item in current.Contents) {
                        if (item is not Directory dir || !dir.Name.Equals(splitLine[2])) {
                            continue;
                        }

                        current = dir;
                        break;
                    }

                    break;
                }
                case "ls": {
                    while (queue.Count > 0) {
                        line = queue.Peek();
                        splitLine = line.Split(' ');
                        if (splitLine[0].Equals("$")) {
                            break;
                        }

                        queue.Dequeue();

                        if (splitLine[0].Equals("dir")) {
                            current.Contents.Add(new Directory { Name = splitLine[1], Parent = current });
                        } else {
                            var file = new File { Name = splitLine[1], Parent = current };
                            file.SetSize(int.Parse(splitLine[0]));
                            current.Contents.Add(file);
                        }
                    }

                    break;
                }
            }
        }

        return root;
    }

    private abstract class Item {
        public abstract int Size { get; }
        public Directory Parent = null;
        public string Name;
    }

    private class Directory : Item {
        public List<Item> Contents = new();
        public override int Size {
            get {
                var sum = 0;
                foreach (var t in Contents) {
                    sum += t.Size;
                }

                return sum;
            }
        }

        public void Calc() {
            int sum = 0;
            Calc(0, ref sum);
            Console.WriteLine(sum);
        }

        private void Calc(int indentation, ref int runningTotal) {
            foreach (var item in Contents) {
                if (item is File) {
                    Console.WriteLine($"{new string('\t', indentation)} {item.Name} [{item.Size}]");
                } else if (item is Directory dir) {
                    var append = "";
                    if (item.Size <= 100000) {
                        append = " <----- ";
                        runningTotal += item.Size;
                    }

                    Console.WriteLine($"{new string('\t', indentation)} {item.Name} [{item.Size}]{append}");
                    dir.Calc(indentation + 1, ref runningTotal);
                }
            }
        }

        private void FindSmallestChildWithAtLeastSpace(int space, ref Directory? smallest) {
            if (Size >= space && (smallest == null || Size < smallest.Size)) {
                smallest = this;
            }

            foreach (var child in Contents) {
                if (child is Directory childDir) {
                    childDir.FindSmallestChildWithAtLeastSpace(space, ref smallest);
                }
            }
        }

        public Directory? FindSmallestChildWithAtLeastSpace(int space) {
            Directory? smallest = null;
            FindSmallestChildWithAtLeastSpace(space, ref smallest);
            return smallest;
        }
    }

    private class File : Item {
        private int size;
        public override int Size {
            get { return size; }
        }

        public void SetSize(int size) {
            this.size = size;
        }
    }
}