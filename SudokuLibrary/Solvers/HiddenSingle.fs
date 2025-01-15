namespace SudokuLibrary.Solvers

open SudokuLibrary
open SudokuLibrary.Types

type HiddenSingle() =
    interface ISudokuStrategy with
        member this.Name = "Hidden Single"
        member this.AttemptStrategy board =
            
            let checkCell (sectionCandidates: Set<int>) (cell: SudokuCell) =
                let difference = Set.difference cell.Candidates sectionCandidates
                
                match difference.Count with
                        | 1 -> Helpers.UpdateCandidates cell difference
                        | _ -> cell
            
            let organiseCellWithSection (section: SudokuCell list) (cell:SudokuCell) =
                List.where (fun (c: SudokuCell) -> c <> cell) section
                |> List.map (_.Candidates)
                |> Set.unionMany
                |> fun candidates -> checkCell candidates cell
                
            let checkSection (section: SudokuCell list) =
                List.map (organiseCellWithSection section) section
            
            let checkSections (sections : SudokuCell list list) =
                List.map (checkSection) sections
                |> List.concat
            
            board
            |> fun b -> SudokuBoard(checkSections b.GetRows)
            |> fun b -> SudokuBoard(checkSections b.GetColumns)
            |> fun b -> SudokuBoard(checkSections b.GetBoxes)
            |> SudokuStrategyAttempt