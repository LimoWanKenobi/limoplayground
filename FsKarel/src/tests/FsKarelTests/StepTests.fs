module FsKarel.Core.Execution.Actions.StepTests

open NUnit.Framework
open FsUnit
open FsKarel.Core
open FsKarel.Core.Execution.Actions

let fail() = true |> should be False
let success() = true |> should be True

// TODO: Improve and split this test and move to its own file
[<Test>]
let ``Addition of walls to a world should work``() =
  let world = World.create (Karel.create (1,2) East 0) (10, 10)

  world.walls |> should equal Map.empty

  let pos = (3, 3)
  let worldWithWall = World.addWall pos WallPositions.North world

  Map.containsKey pos worldWithWall.walls |> should be True
  let wall = Map.find pos worldWithWall.walls

  wall |> should equal WallPositions.North

  let worldWith2Walls = World.addWall pos WallPositions.South worldWithWall

  Map.containsKey pos worldWith2Walls.walls |> should be True
  let walls = Map.find pos worldWith2Walls.walls

  walls |> should equal (WallPositions.North ||| WallPositions.South)

[<Test>]
let ``hasWall should work``() =
  let world = World.create (Karel.create (1,2) East 0) (10, 10)

  let pos = (3, 3)
  World.hasWall pos WallPositions.North world |> should be False

  let worldWithWalls = World.addWall pos WallPositions.North world
  World.hasWall pos WallPositions.North worldWithWalls |> should be True

[<Test>]
let ``The world should have walls in its border``() =
  // TODO: the world should have walls in its border
  1 |> should equal 1

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
  let pos = (2,2)
  let world = World.create (Karel.create pos West 0) (10, 10)
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
  let world = World.create (Karel.create (1,2) West 0) (10, 10)

  let world = assertTurnLeft world East
  let world = assertTurnLeft world North
  assertTurnLeft world West
