namespace SudokuLibrary.Types

[<Struct>]
type SudokuStrategyAttempt (board: SudokuBoard) = 
    member this.Board = SudokuBoard(List.map(SudokuLibrary.Helpers.ClearUpdated) board.Cells)
    member this.Success = List.exists(fun (cell: SudokuCell) -> cell.Updated) board.Cells

type ISudokuStrategy =
    abstract Name: string
    abstract AttemptStrategy: SudokuBoard -> SudokuStrategyAttempt