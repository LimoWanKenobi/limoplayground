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
  let world = World.create (Karel.create (1u ,2u) East 0) (10u, 10u)

  world.walls |> should equal Map.empty

  let pos = (3u, 3u)
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
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)

  let pos = (3u, 3u)
  World.hasWall pos WallPositions.North world |> should be False

  let worldWithWalls = World.addWall pos WallPositions.North world
  World.hasWall pos WallPositions.North worldWithWalls |> should be True

[<Test>]
let ``The world should have walls in its South border``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  
  // Inner walls
  for i = 1 to 8 do
    // West wall
    World.hasWall (uint32 i, 0u) WallPositions.West world  |> should be False 
    World.hasWall (uint32 i, 0u) WallPositions.South world |> should be True
    World.hasWall (uint32 i, 0u) WallPositions.East world  |> should be False
    World.hasWall (uint32 i, 0u) WallPositions.North world |> should be False
  
[<Test>]
let ``The world should have walls in its West border``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  
  // Inner walls
  for i = 1 to 8 do
    // South wall
    World.hasWall (0u, uint32 i) WallPositions.West world  |> should be True 
    World.hasWall (0u, uint32 i) WallPositions.South world |> should be False
    World.hasWall (0u, uint32 i) WallPositions.East world  |> should be False
    World.hasWall (0u, uint32 i) WallPositions.North world |> should be False
    
[<Test>]
let ``The world should have walls in its North border``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  
  // Inner walls
  for i = 1 to 8 do
    // East wall
    World.hasWall (uint32 i, 9u) WallPositions.West world  |> should be False 
    World.hasWall (uint32 i, 9u) WallPositions.South world |> should be False
    World.hasWall (uint32 i, 9u) WallPositions.East world  |> should be False
    World.hasWall (uint32 i, 9u) WallPositions.North world |> should be True

[<Test>]
let ``The world should have walls in its East border``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  
  // Inner walls
  for i = 1 to 8 do
    // North wall
    World.hasWall (9u, uint32 i) WallPositions.West world  |> should be False 
    World.hasWall (9u, uint32 i) WallPositions.South world |> should be False
    World.hasWall (9u, uint32 i) WallPositions.East world  |> should be True
    World.hasWall (9u, uint32 i) WallPositions.North world |> should be False
    
[<Test>]
let ``The world should have walls in its (0,0) corner``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  
  // Corners
  World.hasWall (0u, 0u) WallPositions.South world |> should be True
  World.hasWall (0u, 0u) WallPositions.West world  |> should be True
  World.hasWall (0u, 0u) WallPositions.East world  |> should be False
  World.hasWall (0u, 0u) WallPositions.North world |> should be False
  
[<Test>]
let ``The world should have walls in its (w,0) corner``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  
  // Corners
  World.hasWall (9u, 0u) WallPositions.South world |> should be True
  World.hasWall (9u, 0u) WallPositions.West world  |> should be False
  World.hasWall (9u, 0u) WallPositions.East world  |> should be True
  World.hasWall (9u, 0u) WallPositions.North world |> should be False
  
[<Test>]
let ``The world should have walls in its (0,h) corner``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  
  // Corners
  World.hasWall (0u, 9u) WallPositions.South world |> should be False
  World.hasWall (0u, 9u) WallPositions.West world  |> should be True
  World.hasWall (0u, 9u) WallPositions.East world  |> should be False
  World.hasWall (0u, 9u) WallPositions.North world |> should be True

[<Test>]
let ``The world should have walls in its (w,h) corner``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  
  // Corners
  World.hasWall (9u, 9u) WallPositions.South world |> should be False
  World.hasWall (9u, 9u) WallPositions.West world  |> should be False
  World.hasWall (9u, 9u) WallPositions.East world  |> should be True
  World.hasWall (9u, 9u) WallPositions.North world |> should be True

[<Test>]
let ``Step should increment x when moving to the east``() =
  let world = World.create (Karel.create (1u, 2u) East 0) (10u, 10u)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (2u, 2u)
  | Error _ -> fail()

[<Test>]
let ``Step should increment y when moving to the north``() =
  let world = World.create (Karel.create (1u, 2u) North 0) (10u, 10u)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (1u, 3u)
  | Error _ -> fail()

[<Test>]
let ``Step should decrement y when moving to the south``() =
  let world = World.create (Karel.create (1u, 2u) South 0) (10u, 10u)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (1u, 1u)
  | Error _ -> true |> should be False

[<Test>]
let ``Step should decrement w when moving to the west``() =
  let world = World.create (Karel.create (1u, 2u) West 0) (10u, 10u)
  let result = step world

  match result with
  | Success newWorld -> newWorld.karel.position |> should equal (0u, 2u)
  | Error _ -> fail()

[<Test>]
let ``Step should return an error when trying to step through a wall``() =
  let pos = (2u, 2u)
  let world = World.create (Karel.create pos West 0) (10u, 10u)
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
  let world = World.create (Karel.create (1u, 2u) West 0) (10u, 10u)

  let world = assertTurnLeft world East
  let world = assertTurnLeft world North
  assertTurnLeft world West

[<Test>]
let ``turnoff works``() =
  let world = World.create (Karel.create (2u, 2u) West 0) (10u, 10u)
  let result = turnOff world

  match result with
  | Success newWorld -> newWorld.karel.isOn |> should be False
  | Error _ -> fail()
