module FsKarel.Core.Execution.Actions.StepTests

open NUnit.Framework
open FsUnit
open FsKarel.Core
open FsKarel.Core.Execution.Actions


let createKarel position orientation =
    let karel = {
        position = position
        orientation = orientation
        beepersInBag = 0
    }
    karel

[<Test>]
let ``Step should increment x when moving to the east``() =
    let world = {
        karel = createKarel (1,2) East
        dimensions = (10, 10)
    }

    let result, newWorld = step world
    
    newWorld.karel.position |> should equal (2,2)
    
[<Test>]
let ``Step should increment y when moving to the north``() =
    let world = {
        karel = createKarel (1,2) North
        dimensions = (10, 10)
    }
    let result, newWorld = step world
    
    newWorld.karel.position |> should equal (1,3)
    
[<Test>]
let ``Step should decrement y when moving to the south``() =
    let world = {
        karel = createKarel (1,2) South
        dimensions = (10, 10)
    }
    let result, newWorld = step world
    
    newWorld.karel.position |> should equal (1,1)
    
[<Test>]
let ``Step should decrement w when moving to the west``() =
    let world = {
        karel = createKarel (1,2) West
        dimensions = (10, 10)
    }
    let result, newWorld = step world
    
    newWorld.karel.position |> should equal (0,2)
    
let isError result =
    match result with
    | Error _ -> true
    | _ -> false

[<Test>]
let ``Step should return an error when trying to step outside of the world``() =
    let world = {
        karel = createKarel (0,2) West
        dimensions = (10, 10)
    }
    let result, newWorld = step world
    
    isError result |> should equal true