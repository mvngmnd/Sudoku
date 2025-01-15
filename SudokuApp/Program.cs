namespace SudokuApp;

using SudokuLibrary.Types;
using SudokuLibrary.Solvers;

static class Program
{
    static void Main(string[] args)
    {
        var board = SudokuParser.Parse("nytimes-easy-20230515.txt");
        DrawBoard(board);

        // Ordered from simple to hard
        var strategies = new List<ISudokuStrategy>()
        {
            new SimpleCandidateReduce(),
            new NakedSingle(),
            new HiddenSingle()
        };
        
        Attempt(strategies, board);
    }

    static void Attempt(IList<ISudokuStrategy> strategies, SudokuBoard oldState)
    {
        var ongoingState = oldState;
        
        for (var i = 0; i < strategies.Count;)
        {
            var strategy = strategies[i];
            var attempt = strategy.AttemptStrategy(ongoingState);
            
            if (!attempt.Success)
            {
                i++;
                continue;
            }
            
            while (attempt.Success)
            {
                ongoingState = attempt.Board;
                DrawAttempt(attempt, strategy);
                attempt = strategy.AttemptStrategy(ongoingState);
            }

            if (i > 0)
            {
                i = 0;
            }
        }
    }
    
    static void DrawAttempt(SudokuStrategyAttempt attempt, ISudokuStrategy strategy)
    {
        Console.WriteLine(strategy.Name);
        DrawBoard(attempt.Board);
    }

    static void DrawBoard(SudokuBoard board)
    {
        var rows = board.GetRows.ToList();
        foreach (var row in rows)
        {
            var rowIndex = 0;
            var cells = row.ToList();
            foreach (var cell in cells)
            {
                var val = cell.Value;
                if (val > 0)
                {
                    if (!cell.InitialValue)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(cell.Value);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(cell.Candidates.Count);
                    Console.ResetColor();
                }
                var col = cell.Position.Column;
                if (col % 3 == 2 && col < 8)
                {
                    Console.Write("|");
                }

                rowIndex = cell.Position.Row;
            }

            Console.WriteLine(rowIndex % 3 == 2 && rowIndex < 8 ? "\n---|---|---" : "");
        }
        Console.WriteLine();
    }
}



