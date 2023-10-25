namespace Wheelzy.Solutions;

public static class ChessKnightMoves
{
    // Define possible relative movements for a knight
    private static readonly int[] RowMoves = {2, 2, 1, 1, -1, -1, -2, -2};
    private static readonly int[] ColMoves = {1, -1, 2, -2, 2, -2, 1, -1};

    public static void Exec()
    {
        var moves = GetKnightMoves(3, 7);
        foreach (var move in moves)
        {
            Console.WriteLine($"Possible move: ({move.Item1}, {move.Item2})");
        }
    }
    
    public static List<Tuple<int, int>> GetKnightMoves(int currentRow, int currentCol)
    {
        // Args must be between (0-7)
        // In case the knight not in the board
        if (!IsValidPosition(currentRow, currentCol)) return new List<Tuple<int, int>>();
        
        var possibleMoves = new List<Tuple<int, int>>();

        for (var i = 0; i < 8; i++)
        {
            var newRow = currentRow + RowMoves[i];
            var newCol = currentCol + ColMoves[i];

            // Check if the new position is on the board (assuming an 8x8 chessboard)
            if (IsValidPosition(newRow, newCol))
            {
                possibleMoves.Add(new Tuple<int, int>(newRow, newCol));
            }
        }

        return possibleMoves;
    }

    private static bool IsValidPosition(int newRow, int newCol)
    {
        return newRow is >= 0 and < 8 && newCol is >= 0 and < 8;
    }
}