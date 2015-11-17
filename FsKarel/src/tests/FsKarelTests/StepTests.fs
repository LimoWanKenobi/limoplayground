module FsKarel.Core.Execution.Actions.StepTests

open NUnit.Framework
open FsUnit
open FsKarel.Core
open FsKarel.Core.Execution.Actions

let fail() = true |> should be False
let success() = true |> should be True

[<Test>]
let ``Step should increment x when moving to the east``() =
    let world = World.create (Karel.create (1,2) East 0) (10, 10)
    let result = step world

    match result with
    | Success newWorld -> newWorld.karel.position |> should equal (2,2)
    | Error _ -> fail()

[<Test>]
let ``Step should increment y when moving to the north``() =
  let world = World.create (Karel.create (1,2) North 0) (10, 10)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (1,3)
  | Error _ -> fail()

[<Test>]
let ``Step should decrement y when moving to the south``() =
  let world = World.create (Karel.create (1,2) South 0) (10, 10)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (1,1)
  | Error _ -> true |> should be False

[<Test>]
let ``Step should decrement w when moving to the west``() =
  let world = World.create (Karel.create (1,2) West 0) (10, 10)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (0,2)
  | Error _ -> fail()


[<Test>]
let ``Step should return an error when trying to step through a wall``() =
  let world = World.create (Karel.create (0,2) West 0) (10, 10)
  let result = step world

  match result with
  | Success _ -> fail()
  | Error _ -> success()
