namespace Day7;

internal record Game(List<char> Cards, int Bid) : IComparable
{
    private readonly List<char> _ranking = ['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'];
    private readonly bool _isPart2;

    public Game(List<char> cards, int bid, bool isPart2) : this(cards, bid)
    {
        _isPart2 = isPart2;
        if (isPart2)
            _ranking = ['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'];
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Game gameOther)
            throw new NotImplementedException("Cannot compare to " + (obj?.GetType().Name ?? "null"));

        var gameScore = CalcScore(Cards, _isPart2);
        var gameScoreOther = CalcScore(gameOther.Cards, _isPart2);

        if (gameScore == gameScoreOther)
        {
            for (var i = 0; i < Cards.Count; i++)
            {
                var a = _ranking.IndexOf(Cards[i]);
                var b = _ranking.IndexOf(gameOther.Cards[i]);

                // Must flip comparison because the higher rank is first in the list.
                if (a < b)
                    return 1;
                if (a > b)
                    return -1;
            }

            throw new ArgumentException("Both cards are equal! I don't know what to do...");
        }

        return gameScore > gameScoreOther ? 1 : -1;
    }
    
    static int CalcScore(List<char> cards, bool isPart2)
    {
        var list = new List<(char Label, int Count)>();
        foreach (var label in cards)
        {
            var index = list.FindIndex(x => label == x.Label);
            if (index == -1)
                list.Add((label, 1));
            else
                list[index] = (list[index].Label, list[index].Count + 1);
        }

        list = list.OrderByDescending(x => x.Count).ToList();
        
        if (isPart2)
        {
            // Handle jokers. Don't do this if all labels are jokers (that would result in an exception)
            if (list.Any(x => x.Label == 'J') && list.Any(x => x.Label != 'J'))
            {
                var jokerIndex = list.FindIndex(x => x.Label == 'J');
                var jokerCount = list[jokerIndex].Count;

                var firstNonJokerIndex = list.FindIndex(x => x.Label != 'J');
                list[firstNonJokerIndex] = (list[firstNonJokerIndex].Label, list[firstNonJokerIndex].Count + jokerCount);
                
                list.RemoveAt(jokerIndex);
            }
        }

        if (list.Count == 1) // Five of a kind
            return 10;
        if (list[0].Count == 4) // Four of a kind
            return 9;
        if (list[0].Count == 3 && list[1].Count == 2) // Full house
            return 8;
        if (list[0].Count == 3) // Three of a kind
            return 7;
        if (list[0].Count == 2 && list[1].Count == 2) // Two pair
            return 6;
        if (list[0].Count == 2) // One pair
            return 5;
        // High card
        return 4;
    }
}