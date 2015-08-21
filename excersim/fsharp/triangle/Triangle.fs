module Triangle

type TriangleKind =
    Equilateral
    | Isosceles
    | Scalene

type Triangle(a: decimal, b: decimal, c: decimal) =
    let sides = [a; b; c]

    let sum = List.sum sides

    let isValid = 
        sides 
        |> List.map (fun x -> sum - x > x)
        |> List.reduce (&&)

    let equalSides = Seq.groupBy id sides

    member this.Kind() = 
        if not isValid then raise (System.InvalidOperationException())

        match Seq.length equalSides with
        | 3 -> TriangleKind.Scalene
        | 2 -> TriangleKind.Isosceles
        | 1 -> TriangleKind.Equilateral
        | _ -> raise (System.InvalidOperationException())

       