namespace Day7;

abstract class Path
{
    public string name;

    protected Path(string name)
    {
        this.name = name;
    }

    public abstract int CalcSize();
}

class Directory : Path
{
    public List<Path> Paths;

    public override int CalcSize() => 
        Paths.Sum(x => x.CalcSize());

    public Directory(string name, List<Path>? paths = null) : base(name)
    {
        Paths = paths ?? new();
    }
}

class File : Path
{
    public int size;
    public override int CalcSize() => size;

    public File(string name, int size) : base(name)
    {
        this.size = size;
    }
}