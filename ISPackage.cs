namespace Sims1Pkg;

public interface ISPackage
{
    void Extract(string file);

    void Compress(string file);
}

public interface ISMetdata
{
    string Name { get; set; }
    string Author { get; set; }
    string Date { get; set; }
}