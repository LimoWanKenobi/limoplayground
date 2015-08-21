module Triangle

type TriangleKind =
    Equilateral
    | Isosceles
    | Scalene

type Triangle(a: decimal, b: decimal, c: decimal) =
    let sides = [a; b; c]

    let sum = a + b + c

    let isValidSide side = sum - side > side

    let isValid = 
        sides 
        |> List.map isValidSide
        |> List.reduce (&&)

    let equalSides = 
        sides
        |> Seq.distinct
        |> Seq.length

    member this.Kind() = 
        if not isValid then raise (System.InvalidOperationException())

        match equalSides with
        | 3 -> TriangleKind.Scalene
        | 2 -> TriangleKind.Isosceles
        | 1 -> TriangleKind.Equilateral
        | _ -> raise (System.InvalidOperationException())

       