module FsKarel.Core.Tests

open NUnit.Framework
open FsUnit
open FsKarel.Core

[<Test>]
let ``Everything seems to be correctly configured``() =
    let k = { position = (1,2); orientation = West; beepersInBag = 0 }

    let w = { karel = k }
    w.add() |> should equal 3