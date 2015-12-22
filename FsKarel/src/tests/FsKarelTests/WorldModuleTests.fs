module FsKarel.Core.WorldModuleTests

open NUnit.Framework
open FsUnit
open FsKarel.Core

let fail() = true |> should be False
let success() = true |> should be True

[<Test>]
let ``Addition of walls to a world should work``() =
  let world = World.Default
  world.walls |> should equal Map.empty

  let pos = (3u, 3u)
  let result = World.addWall pos WallPositions.North world
  match result with
  | Error _ -> fail()
  | Success worldWithWall ->
    Map.containsKey pos worldWithWall.walls |> should be True
    let wall = Map.find pos worldWithWall.walls
    wall |> should equal WallPositions.North

    let result2 = World.addWall pos WallPositions.South worldWithWall

    match result2 with
    | Error _ -> fail()
    | Success worldWith2Walls ->
      Map.containsKey pos worldWith2Walls.walls |> should be True
      let walls = Map.find pos worldWith2Walls.walls

      walls |> should equal (WallPositions.North ||| WallPositions.South)
  
[<Test>]
let ``Adding a wall outside of the world should fail``() =
  let world = World.Default
  
  let pos = (200u, 200u)
  let result = World.addWall pos WallPositions.North world
  match result with 
  | Error _ -> success()
  | Success _ -> fail()

[<Test>]
let ``hasWall should work``() =
  let world = World.Default
  let pos = (3u, 3u)
  World.hasWall pos WallPositions.North world |> should be False

  let result = World.addWall pos WallPositions.North world
  
  match result with
  | Error _ -> fail()
  | Success worldWithWalls -> World.hasWall pos WallPositions.North worldWithWalls |> should be True

[<Test>]
let ``The world should have walls in its South border``() =
  let world = World.create Karel.Default (10u, 10u)
  
  // Inner walls
  for i = 1 to 8 do
    // West wall
    World.hasWall (uint32 i, 0u) WallPositions.West world  |> should be False 
    World.hasWall (uint32 i, 0u) WallPositions.South world |> should be True
    World.hasWall (uint32 i, 0u) WallPositions.East world  |> should be False
    World.hasWall (uint32 i, 0u) WallPositions.North world |> should be False
  
[<Test>]
let ``The world should have walls in its West border``() =
  let world = World.create Karel.Default (10u, 10u)
  
  // Inner walls
  for i = 1 to 8 do
    // South wall
    World.hasWall (0u, uint32 i) WallPositions.West world  |> should be True 
    World.hasWall (0u, uint32 i) WallPositions.South world |> should be False
    World.hasWall (0u, uint32 i) WallPositions.East world  |> should be False
    World.hasWall (0u, uint32 i) WallPositions.North world |> should be False
    
[<Test>]
let ``The world should have walls in its North border``() =
  let world = World.create Karel.Default (10u, 10u)
  
  // Inner walls
  for i = 1 to 8 do
    // East wall
    World.hasWall (uint32 i, 9u) WallPositions.West world  |> should be False 
    World.hasWall (uint32 i, 9u) WallPositions.South world |> should be False
    World.hasWall (uint32 i, 9u) WallPositions.East world  |> should be False
    World.hasWall (uint32 i, 9u) WallPositions.North world |> should be True

[<Test>]
let ``The world should have walls in its East border``() =
  let world = World.create Karel.Default (10u, 10u)
  
  // Inner walls
  for i = 1 to 8 do
    // North wall
    World.hasWall (9u, uint32 i) WallPositions.West world  |> should be False 
    World.hasWall (9u, uint32 i) WallPositions.South world |> should be False
    World.hasWall (9u, uint32 i) WallPositions.East world  |> should be True
    World.hasWall (9u, uint32 i) WallPositions.North world |> should be False
    
[<Test>]
let ``The world should have walls in its (0,0) corner``() =
  let world = World.create Karel.Default (10u, 10u)
  
  // Corners
  World.hasWall (0u, 0u) WallPositions.South world |> should be True
  World.hasWall (0u, 0u) WallPositions.West world  |> should be True
  World.hasWall (0u, 0u) WallPositions.East world  |> should be False
  World.hasWall (0u, 0u) WallPositions.North world |> should be False
  
[<Test>]
let ``The world should have walls in its (w,0) corner``() =
  let world = World.create Karel.Default (10u, 10u)
  
  // Corners
  World.hasWall (9u, 0u) WallPositions.South world |> should be True
  World.hasWall (9u, 0u) WallPositions.West world  |> should be False
  World.hasWall (9u, 0u) WallPositions.East world  |> should be True
  World.hasWall (9u, 0u) WallPositions.North world |> should be False
  
[<Test>]
let ``The world should have walls in its (0,h) corner``() =
  let world = World.create Karel.Default (10u, 10u)
  
  // Corners
  World.hasWall (0u, 9u) WallPositions.South world |> should be False
  World.hasWall (0u, 9u) WallPositions.West world  |> should be True
  World.hasWall (0u, 9u) WallPositions.East world  |> should be False
  World.hasWall (0u, 9u) WallPositions.North world |> should be True

[<Test>]
let ``The world should have walls in its (w,h) corner``() =
  let world = World.create Karel.Default (10u, 10u)
  
  // Corners
  World.hasWall (9u, 9u) WallPositions.South world |> should be False
  World.hasWall (9u, 9u) WallPositions.West world  |> should be False
  World.hasWall (9u, 9u) WallPositions.East world  |> should be True
  World.hasWall (9u, 9u) WallPositions.North world |> should be True
