module DifferenceOfSquares

type DifferenceOfSquares(num) =
    let square x = pown x 2

    let numbers = [1 .. num]

    let sqrSums = 
        numbers
        |> List.sum
        |> square

    let sumSqrs = 
        numbers
        |> List.sumBy square

    let diff = sqrSums - sumSqrs

    member this.squareOfSums() =
        sqrSums
        
    member this.sumOfSquares() =
        sumSqrs

    member this.difference() = 
        diff