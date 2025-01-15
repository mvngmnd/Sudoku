namespace SudokuLibrary.Solvers

open SudokuLibrary
open SudokuLibrary.Types

type NakedSingle() =
    interface ISudokuStrategy with
        member this.Name = "Naked Single"
        member this.AttemptStrategy board =
            
            let checkCell (cell: SudokuCell) =
                match cell.Candidates.Count with
                | 1 -> Helpers.UpdateValue cell (Seq.head cell.Candidates)
                | _ -> cell
                
            SudokuBoard(List.map(checkCell) board.Cells)
            |> SudokuStrategyAttempt