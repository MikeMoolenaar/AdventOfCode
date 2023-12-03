using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<AoCBenchmark>();

[MemoryDiagnoser(displayGenColumns: false)]
public class AoCBenchmark
{
    private List<string> lines = File.ReadAllLines("Input.txt").ToList();
    private string input = File.ReadAllText("Input.txt");

    [Benchmark]
    public void MySolution()
    {

    }

    [Benchmark]
    public void OtherSolution()
    {
       
    }
}