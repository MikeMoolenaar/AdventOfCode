var watch = System.Diagnostics.Stopwatch.StartNew();
var lines = File.ReadLines("Input.txt").ToArray();



watch.Stop();
Console.WriteLine($"\nFinished in {watch.ElapsedMilliseconds}ms");