module FsKarel.Core.Tests

open NUnit.Framework
open FsUnit
open FsKarel.Core

[<Test>]
let ``Everything seems to be correctly configured``() =
    let t = { k_x = 1; k_y = 2 }
    t.add() |> should equal 3