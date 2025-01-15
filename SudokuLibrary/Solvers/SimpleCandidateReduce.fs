namespace SudokuLibrary.Solvers

open SudokuLibrary
open SudokuLibrary.Types

type SimpleCandidateReduce() =
    interface ISudokuStrategy with
        member this.Name = "Simple Candidate Reduce"
        member this.AttemptStrategy board = 
            
            let checkCell (sectionValues: Set<int>) (cell: SudokuCell) =
                let difference = Set.difference cell.Candidates sectionValues
                
                match difference.Count < cell.Candidates.Count with
                        | true -> Helpers.UpdateCandidates cell difference
                        | _ -> cell
            
            let checkSections (sections : SudokuCell list list) =
                List.map (
                    fun section -> (section, List.map (fun (cell: SudokuCell) -> cell.Value) section |> Set.ofList)
                ) sections
                |> List.map (fun (section, values) -> List.map(checkCell values) section)
                |> List.concat
            
            board
            |> fun b -> SudokuBoard(checkSections b.GetRows)
            |> fun b -> SudokuBoard(checkSections b.GetColumns)
            |> fun b -> SudokuBoard(checkSections b.GetBoxes)
            |> SudokuStrategyAttempt