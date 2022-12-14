namespace Provodnik
{
    public class Cur
    {
        public int pos;
        public int max;
    }
    public class Elem
    {
        public Elem(string name1, string path1, bool folder1)
        {
            name = name1;
            path = path1;
            folder = folder1;
        }
        public string name;
        public string path;
        public bool folder;
    }
    public static class Files
    {
        public static List<Elem> Get(string path)
        {
            List<Elem> elems = new List<Elem>();
            foreach (string file in Directory.GetFiles(path))
                elems.Add(new Elem(file.Split("\\")[^1], file, false));
            foreach (string file in Directory.GetDirectories(path))
                elems.Add(new Elem(file.Split("\\")[^1], file, true));
            return elems;
        }
    }
}