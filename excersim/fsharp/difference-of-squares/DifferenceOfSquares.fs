module DifferenceOfSquares

type DifferenceOfSquares(num: int) =
    let square x = x * x

    member this.squareOfSums() =
        [1 .. num] 
        |> List.sum
        |> square
        

    member this.sumOfSquares() =
        [1 .. num]
        |> List.map square
        |> List.sum

    member this.difference() =
        this.squareOfSums() - this.sumOfSquares()