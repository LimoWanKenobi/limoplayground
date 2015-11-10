module FsKarel.Core.Execution.Actions.StepTests

open NUnit.Framework
open FsUnit
open FsKarel.Core
open FsKarel.Core.Execution.Actions

[<Test>]
let ``Step should move karel one step in its orientation``() =
    let k = { position = (1,2); orientation = West; beepersInBag = 0 }
    let w = { karel = k }
    
    let result = step w
    
    result.result.karel.position |> should equal (2,2) 