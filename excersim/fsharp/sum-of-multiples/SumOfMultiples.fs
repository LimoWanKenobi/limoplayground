module SumOfMultiples

type SumOfMultiples(numbers: int list) =
    new() = SumOfMultiples([3; 5])

    member this.To limit =
        numbers
        |> List.map (fun n -> seq {0 .. n .. limit - 1})
        |> Seq.concat
        |> Seq.distinct
        |> Seq.sum
