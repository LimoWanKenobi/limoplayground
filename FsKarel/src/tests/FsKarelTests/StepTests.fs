module FsKarel.Core.Execution.Actions.StepTests

open NUnit.Framework
open FsUnit
open FsKarel.Core
open FsKarel.Core.Execution.Actions

let fail() = true |> should be False
let success() = true |> should be True

[<Test>]
let ``Step should increment x when moving to the east``() =
  let world = World.create (Karel.create (1u, 2u) East 0u) (10u, 10u)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (2u, 2u)
  | Error _ -> fail()

[<Test>]
let ``Step should increment y when moving to the north``() =
  let world = World.create (Karel.create (1u, 2u) North 0u) (10u, 10u)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (1u, 3u)
  | Error _ -> fail()

[<Test>]
let ``Step should decrement y when moving to the south``() =
  let world = World.create (Karel.create (1u, 2u) South 0u) (10u, 10u)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (1u, 1u)
  | Error _ -> true |> should be False

[<Test>]
let ``Step should decrement w when moving to the west``() =
  let world = World.create (Karel.create (1u, 2u) West 0u) (10u, 10u)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (0u, 2u)
  | Error _ -> fail()

[<Test>]
let ``Step should return an error when trying to step through a wall``() =
  let pos = (2u, 2u)
  let world = World.create (Karel.create pos West 0u) (10u, 10u)
              |> World.addWall pos WallPositions.West

  let result = step world

  match result with
  | Success _ -> fail()
  | Error _ -> success()

let assertTurnLeft world expectedOrientation =
  world.karel.orientation |> should not' (equal expectedOrientation)
  let result = turnLeft world

  match result with
  | Success newWorld ->
    newWorld.karel.orientation |> should equal expectedOrientation
    newWorld
  | Error _ ->
    fail()
    world

let ``turnLeft tests``() =
  let world = World.create (Karel.create (1u, 2u) West 0u) (10u, 10u)

  let world = assertTurnLeft world East
  let world = assertTurnLeft world North
  assertTurnLeft world West

[<Test>]
let ``turnoff works``() =
  let world = World.create (Karel.create (2u, 2u) West 0u) (10u, 10u)
  let result = turnOff world

  match result with
  | Success newWorld -> newWorld.karel.isOn |> should be False
  | Error _ -> fail()
