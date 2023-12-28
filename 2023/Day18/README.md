# Day 18
- [AoC website](https://adventofcode.com/2023/day/18)
- Solve time: 2h0m

Yay a fun and not-extremely-hard problem today! Brute forced part 1, but finally learned about the Shoelace formula for solving part 2 (and thew away the bruteforce solution). I did look at reddit because I didn't know what formula to use, and saw a link of [a nice explanation of the shoelace](https://www.themathdoctors.org/polygon-coordinates-and-areas/) formula there, which I implemented myself. Now I also know how to implement day 10 part 2, I was too afraid to use the shoelace formula because the maths of wikipedia looked too complicated, but thanks to today I can do it!  

Some optimisations:  

| Optimisation                                                  | Execution time |
|---------------------------------------------------------------|----------------|
| -                                                             | 30ms           |
| Define regex to parse each step outside the foreach line loop | 27ms           |
| Source generated regex                                        | 27ms           |
| Change all lists -> arrays                                    | 25ms           |

Keep in mind the Console.Writeline's takes about 8ms.  
Very small optimisations, I didn't apply the source generated regex because the code looked less readable without a performance improvement.