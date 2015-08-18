module DifferenceOfSquares

type DifferenceOfSquares(num) =
    let square x = x * x

    member this.squareOfSums() =
        [1 .. num] 
        |> List.sum
        |> square
        
    member this.sumOfSquares() =
        [1 .. num]
        |> List.sumBy square

    member this.difference() =
        this.squareOfSums() - this.sumOfSquares()