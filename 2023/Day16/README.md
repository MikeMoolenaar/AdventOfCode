# Day 16
- [AoC website](https://adventofcode.com/2023/day/16)
- Solve time: 4h30m

Realy fun problem today! Had some trouble imagining what direction the light should take, ended up drawing it (which I should have done sooner). Another mistake I made was the loop detection, I checked first if node has been visited 5 times before and the stop processing, but this didn't work :(. Somebody on reddit mentioned tracking the splits instead, and that worked! So now we visit each split once, but we can visit every other note unlimited times!  
And then part 2 came, so I thought oh 2 simple for loops, shouldn't be that hard?? Oh boiii, I forgot that adding the maxX and maxY positions should not be the array length, but arrayLength minus one! This took me about 90 minutes. So I need to drill in my brain that the last item in array is `length - 1`.  

Anyway, I then did some optimisations:

| Optimisation                                                                                         | Execution time |
|------------------------------------------------------------------------------------------------------|----------------|
| -                                                                                                    | 560ms          |
| Use range check instead of returning on IndexOutOfRangeException                                     | 332ms          |
| Using HashSet instead of a list for visited nodes and splits (this also eliminated`list.Distinct()`) | 283ms          |
| Putting a static list of all of the 4 char dirs in the main scope (outside of the function)          | 272ms          |
| Replace if/else for checking current character to a switch statement                                 | 258ms          |

That's twice as fast! Nice! I should use HashSets more by the way, they are perfect in this use case.  
Also, exceptions impact performance so much, always use if-checks if possible.