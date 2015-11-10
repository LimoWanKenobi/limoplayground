module FsKarel.Core.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Step should move karel one step in its orientation``() =
    1 |> should equal 1