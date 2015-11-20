module FsKarel.Core.KarelModuleTests

open NUnit.Framework
open FsUnit
open FsKarel.Core
open FsKarel.Core.Execution.Actions

let fail() = true |> should be False
let success() = true |> should be True

// TODO: Improve and split this test and move to its own file
[<Test>]
let ``Addition of walls to a world should work``() =
  fail()
  
