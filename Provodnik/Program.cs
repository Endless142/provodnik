using System.Runtime.ConstrainedExecution;

namespace Provodnik;

internal class Program
{
    static void Main(string[] args)
    {
        string path = "";
        List<Elem> elements = new List<Elem>();
        while (true)
        {
            Console.WriteLine("ESC-позволит оказаться курсору вверху!");
            if (path == "")
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in allDrives)
                {
                    double totalSpace = drive.TotalSize;
                    double availableSpace = drive.AvailableFreeSpace;
                    string name = $"Диск {drive.Name}. Доступно {availableSpace} из {totalSpace}";
                    elements.Add(new Elem(name, drive.Name, true));
                }
            }
            else
            {
                elements.Clear();
                elements = Files.Get(path);
            }
            Cur cur = new Cur();
            cur.pos = 1;
            cur.max = 0;
            foreach (Elem elem in elements)
            {
                cur.max++;
                Console.WriteLine("  " + elem.name);
            }
            bool gg = true;
            while (gg)
            {
                Console.SetCursorPosition(0, cur.pos);
                Console.Write(">");
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(0, cur.pos);
                        Console.Write(" ");
                        if (cur.pos != 1)
                            cur.pos--;
                        else
                            cur.pos = cur.max;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(0, cur.pos);
                        Console.Write(" ");
                        if (cur.pos != cur.max)
                            cur.pos++;
                        else
                            cur.pos = 1;
                        break;
                    case ConsoleKey.Enter:
                        gg = false;
                        Elem elem = elements[cur.pos - 1];
                        if (elem.folder)
                        {
                            path = elem.path;
                            Console.Clear();
                        }
                        break;
                    case ConsoleKey.Escape:
                        while (true)
                        {
                            if (path[^1] != '\\')
                                path = path.Remove(path.Length - 1);
                            else
                                break;

                        }
                        Console.Clear();
                        gg = false;
                        elements = Files.Get(path);
                        break;
                }
            }
        }
    }
}