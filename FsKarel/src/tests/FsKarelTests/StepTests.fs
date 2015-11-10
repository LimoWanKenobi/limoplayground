module FsKarel.Core.Execution.Actions.StepTests

open NUnit.Framework
open FsUnit
open FsKarel.Core
open FsKarel.Core.Execution.Actions

[<Test>]
let ``Step should move karel one step in its direction``() =
    let k = { position = (1,2); orientation = East; beepersInBag = 0 }
    let w = { karel = k; dimensions = (10, 10) }
    
    let result = step w
    
    result.result.karel.position |> should equal (2,2) 