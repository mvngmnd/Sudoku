using System.Text;
using Microsoft.FSharp.Collections;

using SudokuLibrary.Types;

namespace SudokuApp;

public static class SudokuParser
{
    private static SudokuCell CreateCell(int value, int index)
    {
        var row = index / 9;
        var column = index % 9;
        var box = row / 3 * 3 + column / 3;
        
        return new SudokuCell(
            new BoardPosition(row, column, box),
            value,
            new FSharpSet<int>(value == 0 ? [1,2,3,4,5,6,7,8,9] : []),
            initialValue: value > 0,
            updated: false
        );
    }
    
    public static SudokuBoard Parse(string file)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Examples", file);
        
        var sr = new StreamReader(fullPath);
        var sb = new StringBuilder();

        var text = sr.ReadLine();

        while (text != null)
        {
            sb.Append(text);
            text = sr.ReadLine();
        }

        var values = sb.ToString()
            .Select(ch => int.TryParse(ch.ToString(), out var val) ? val : default)
            .ToList();

        return new SudokuBoard(
            ListModule.OfSeq(values.Select(CreateCell))
        );
    }
}