namespace SudokuLibrary.Types

open System

type BoardPosition =
    struct
        val Row: int
        val Column: int
        val Box: int
        
        new (row: int, column: int, box: int) = {
            Row = row; Column = column; Box = box
        }
    end

type SudokuCell =
    struct
        val Position : BoardPosition
        val Value: int
        val Candidates: Set<int>
        val InitialValue: bool
        val Updated: bool
        
        new (
            position : BoardPosition,
            value: int,
            candidates: Set<int>,
            initialValue: bool,
            updated: bool
        ) = {
            Position = position
            Value = value
            Candidates = candidates
            InitialValue = initialValue
            Updated = updated
        }
    end

[<Struct>]
type SudokuBoard (cells : SudokuCell list) = 
    member this.Cells = cells
    member this.GetRows =
        List.map(this.GetRow) [0..8]
    member this.GetColumns =
        List.map(this.GetColumn) [0..8]
    member this.GetBoxes =
        List.map(this.GetBox) [0..8]
    member this.GetRow (index: int) =
        List.where(fun (cell: SudokuCell) -> cell.Position.Row = index) this.Cells
    member this.GetColumn (index: int) =
        List.where(fun (cell: SudokuCell) -> cell.Position.Column = index) this.Cells
    member this.GetBox (index: int) =
        List.where(fun (cell: SudokuCell) -> cell.Position.Box = index) this.Cells  