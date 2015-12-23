module FsKarel.Core.WorldModuleTests

open NUnit.Framework
open FsUnit
open FsKarel.Core

let testFail() = true |> should be False
let testSuccess() = true |> should be True

[<Test>]
let ``Addition of walls to a world should work``() =
  let world = World.Default
  let pos = (3u, 3u)
  world.walls |> should equal Map.empty

  let assertAddingWalls expected world =
    Map.containsKey pos world.walls |> should be True
    let wall = Map.find pos world.walls
    wall |> should equal expected
    world

  let addWallNorth = (World.addWall WallPositions.North pos)
  let asserWallNorth = map (assertAddingWalls WallPositions.North)
  let addWallSouth = bind (World.addWall WallPositions.South pos)
  let assertWallsNorthAndSouth = map (assertAddingWalls (WallPositions.North ||| WallPositions.South))

  let testFn = 
    addWallNorth
    >> asserWallNorth
    >> addWallSouth
    >> assertWallsNorthAndSouth
    >> ignore

  testFn world
  
[<Test>]
let ``Adding a wall outside of the world should testFail``() =
  let world = World.Default
  
  let pos = (200u, 200u)
  let result = World.addWall WallPositions.North pos world
  match result with 
  | Failure _ -> testSuccess()
  | Success _ -> testFail()

[<Test>]
let ``hasWall should work``() =
  let world = World.Default
  let pos = (3u, 3u)
  World.hasWall pos WallPositions.North world |> should be False

  let result = World.addWall WallPositions.North pos world
  
  match result with
  | Failure _ ->
     testFail()
  | Success worldWithWalls ->
     World.hasWall pos WallPositions.North worldWithWalls |> should be True

let smallWorld = World.create Karel.Default (10u, 10u)

[<Test>]
let ``The world should have walls in its South border``() =
  // Inner walls
  for i = 1 to 8 do
    // West wall
    World.hasWall (uint32 i, 0u) WallPositions.West smallWorld  |> should be False 
    World.hasWall (uint32 i, 0u) WallPositions.South smallWorld |> should be True
    World.hasWall (uint32 i, 0u) WallPositions.East smallWorld  |> should be False
    World.hasWall (uint32 i, 0u) WallPositions.North smallWorld |> should be False
  
[<Test>]
let ``The world should have walls in its West border``() =
  // Inner walls
  for i = 1 to 8 do
    // South wall
    World.hasWall (0u, uint32 i) WallPositions.West smallWorld  |> should be True 
    World.hasWall (0u, uint32 i) WallPositions.South smallWorld |> should be False
    World.hasWall (0u, uint32 i) WallPositions.East smallWorld  |> should be False
    World.hasWall (0u, uint32 i) WallPositions.North smallWorld |> should be False
    
[<Test>]
let ``The world should have walls in its North border``() =
  // Inner walls
  for i = 1 to 8 do
    // East wall
    World.hasWall (uint32 i, 9u) WallPositions.West smallWorld  |> should be False 
    World.hasWall (uint32 i, 9u) WallPositions.South smallWorld |> should be False
    World.hasWall (uint32 i, 9u) WallPositions.East smallWorld  |> should be False
    World.hasWall (uint32 i, 9u) WallPositions.North smallWorld |> should be True

[<Test>]
let ``The world should have walls in its East border``() =
  // Inner walls
  for i = 1 to 8 do
    // North wall
    World.hasWall (9u, uint32 i) WallPositions.West smallWorld  |> should be False 
    World.hasWall (9u, uint32 i) WallPositions.South smallWorld |> should be False
    World.hasWall (9u, uint32 i) WallPositions.East smallWorld  |> should be True
    World.hasWall (9u, uint32 i) WallPositions.North smallWorld |> should be False
    
[<Test>]
let ``The world should have walls in its (0,0) corner``() =
  // Corners
  World.hasWall (0u, 0u) WallPositions.South smallWorld |> should be True
  World.hasWall (0u, 0u) WallPositions.West smallWorld  |> should be True
  World.hasWall (0u, 0u) WallPositions.East smallWorld  |> should be False
  World.hasWall (0u, 0u) WallPositions.North smallWorld |> should be False
  
[<Test>]
let ``The world should have walls in its (w,0) corner``() =
  // Corners
  World.hasWall (9u, 0u) WallPositions.South smallWorld |> should be True
  World.hasWall (9u, 0u) WallPositions.West smallWorld  |> should be False
  World.hasWall (9u, 0u) WallPositions.East smallWorld  |> should be True
  World.hasWall (9u, 0u) WallPositions.North smallWorld |> should be False
  
[<Test>]
let ``The world should have walls in its (0,h) corner``() =
  // Corners
  World.hasWall (0u, 9u) WallPositions.South smallWorld |> should be False
  World.hasWall (0u, 9u) WallPositions.West smallWorld  |> should be True
  World.hasWall (0u, 9u) WallPositions.East smallWorld  |> should be False
  World.hasWall (0u, 9u) WallPositions.North smallWorld |> should be True

[<Test>]
let ``The world should have walls in its (w,h) corner``() =
  // Corners
  World.hasWall (9u, 9u) WallPositions.South smallWorld |> should be False
  World.hasWall (9u, 9u) WallPositions.West smallWorld  |> should be False
  World.hasWall (9u, 9u) WallPositions.East smallWorld  |> should be True
  World.hasWall (9u, 9u) WallPositions.North smallWorld |> should be True

[<Test>]
let ``Addition of beepers to a world should work``() =
  let world = World.Default
  world.beepers |> should equal Map.empty

  let pos = (3u, 3u)
  let result = World.setBeepers 5u pos world
  match result with
  | Failure _ -> testFail()
  | Success worldWithBeepers ->
    Map.containsKey pos worldWithBeepers.beepers |> should be True
    let beepers = Map.find pos worldWithBeepers.beepers
    beepers |> should equal 5u

    let result2 = World.setBeepers 8u pos worldWithBeepers

    match result2 with
    | Failure _ -> testFail()
    | Success worldWithBeepers2 ->
      Map.containsKey pos worldWithBeepers2.beepers |> should be True
      let beepers = Map.find pos worldWithBeepers2.beepers

      beepers |> should equal 8u
  
[<Test>]
let ``Adding a beeper outside of the world should testFail``() =
  let world = World.Default
  
  let pos = (200u, 200u)
  let result = World.setBeepers 5u pos world
  match result with 
  | Failure _ -> testSuccess()
  | Success _ -> testFail()
  
[<Test>]
let ``hasBeeper should work``() =
  let world = World.Default
  let pos = (3u, 3u)
  match World.hasBeepers pos world with 
    | Failure _ -> testFail()
    | Success r -> r |> should be False

  let result = World.setBeepers 1u pos world
  
  match result with
  | Failure _ -> testFail()
  | Success worldWithBeepers ->
    match World.hasBeepers pos worldWithBeepers with
    | Failure _ -> testFail()
    | Success r -> r |> should be True
    
    match World.hasBeepers (10u, 10u) worldWithBeepers with
    | Failure _ -> testFail()
    | Success r -> r |> should be False

  
(*[<Test>]
let ``Putting a beeper in a position in the world should work``() =
  let world = World.Default
  
  let pos = (3u, 3u)
  let result = World.setBeepers 5u pos world
  match result with
  | Error _ -> testFail()
  | Success worldWithBeepers ->
    Map.containsKey pos worldWithBeepers.beepers |> should be True
    let beepers = Map.find pos worldWithBeepers.beepers
    beepers |> should equal 5u

    let result2 = World.setBeepers 8u pos worldWithBeepers

    match result2 with
    | Error _ -> testFail()
    | Success worldWithBeepers2 ->
      Map.containsKey pos worldWithBeepers2.beepers |> should be True
      let beepers = Map.find pos worldWithBeepers2.beepers

      beepers |> should equal 8u
  
[<Test>]
let ``Putting a beeper outside of the world should testFail``() =
  let world = World.Default
  
  let pos = (200u, 200u)
  let result = World.putBeeper pos world
  match result with 
  | Error _ -> testSuccess()
  | Success _ -> testFail()*)