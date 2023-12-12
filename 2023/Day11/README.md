# Day 11
- [AoC website](https://adventofcode.com/2023/day/11)
- Solve time: 1h20m

After day 10 I needed an easy one, and luckily, day 11 was that day! Made some dumb mistakes, like still using the [test data when calculating the final answers](https://www.reddit.com/r/adventofcode/comments/18fnwna/2023_day_11_part_2_if_its_a_big_number_it_must_be/) and using ints instead of longs, but overall quite happy. I do have a few I-don't-know-whys, like dividing the distances by 2, don't know.  

Didn't learn anything from other solutions this time. I did learn that `List<T>` uses int32 inside and cannot be expanded to int64+. You can use a `HashSet` (doesn't have a limit, though the overall hashset needs to be < 2gb) if you REALLY want bigger lists, but obviously that's not a good idea.