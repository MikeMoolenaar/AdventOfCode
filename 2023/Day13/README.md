# Day 13
- [AoC website](https://adventofcode.com/2023/day/X)
- Solve time: 8h20m

Oh my god 8 hours?? Yes, and all because of a stupid bug. I wasn't string.Join'ing the column (while I was for the rows) and didn't notice for several hours. This bug only arises in part 2 and not part 1, which is why it took so long. Oh well, the important part is that I solved it, with some help from Reddit solutions. Somebody mentioned a trick where you could use the difference between the rows/columns to calculate if it is mirrored, genius!

- Part 1: difference should be 0
- Part 2: difference should be 1 (e.g. it's smudged, because the one remaining value makes the difference 0)