namespace SudokuLibrary
open SudokuLibrary.Types

module Helpers =
    let UpdateCandidates (cell:SudokuCell) (candidates: Set<int>) =
        SudokuCell(
            cell.Position,
            cell.Value,
            candidates,
            cell.InitialValue,
            Set.isProperSubset candidates cell.Candidates 
        )
            
    let UpdateValue (cell:SudokuCell) (value: int) =
        SudokuCell(
            cell.Position,
            value,
            Set.empty,
            cell.InitialValue,
            true
        )
        
    let ClearUpdated (cell:SudokuCell) =
        SudokuCell(
            cell.Position,
            cell.Value,
            cell.Candidates,
            cell.InitialValue,
            false
        )