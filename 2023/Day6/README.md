# Day 6
- [AoC website](https://adventofcode.com/2023/day/6)
- Solve time: 30m

Sheesh that was easy. My solution takes 130ms to run but whatever. Should have come to the solution sooner if I read the solution faster and see the patterns quicker, should try that next time (there's so much text in the description compared to the simple code it produces).

Turns out this was just a Quadratic Formula lol. Just executing that makes it much faster, like encse did in [his solution](https://github.com/encse/adventofcode/blob/master/2023/Day06/Solution.cs). Why didn't I realize that sooner?  
CameronAavik did some [funky parsing](https://github.com/CameronAavik/AdventOfCode/blob/master/csharp/2023/Solvers/Day06.cs#L31) to a `long` using a span and slices, which is faster I presume? Very neat.