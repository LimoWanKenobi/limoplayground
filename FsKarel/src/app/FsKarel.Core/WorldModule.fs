namespace FsKarel.Core

module World =

  let create karel dimensions =
    {
      karel = karel
      dimensions = dimensions
      walls = Map.empty
      beepers = Map.empty
    }

  let addWall position direction (world:WorldState) =
    let wall =
      match Map.containsKey position world.walls with
      | true -> Map.find position world.walls
      | false -> WallPositions.None

    let newWall = (wall ||| direction)

    { world with walls = Map.add position newWall world.walls }

  let hasWall position direction (world:WorldState) =
    let (dw, dh) = world.dimensions
    let (w, h) = position
  
    let wallWest =
      if w = 0u then WallPositions.West
      else WallPositions.None
      
    let wallEast =
      if w = (dw - 1u) then WallPositions.East
      else WallPositions.None
      
    let wallSouth =
      if h = 0u then WallPositions.South 
      else WallPositions.None
      
    let wallNorth =
      if h = (dh - 1u) then WallPositions.North
      else WallPositions.None
      
    let wallsPosition =
      match Map.containsKey position world.walls with
      | true -> Map.find position world.walls
      | false -> WallPositions.None

    let walls = wallsPosition ||| wallWest ||| wallEast ||| wallSouth ||| wallNorth

    direction = (walls &&& direction)
    
  let setBeepers position amount (world:WorldState) =
    world
    
  let putBeeper position (world:WorldState) =
    world
    
  let pickBeeper position (world:WorldState) =
    world
    
  let beepersAtPosition position (world:WorldState) = 
    0u
    
  let hasBeepers position (world:WorldState) =
    false // (beepersAtPosition position world) > 0u
    
  let hasKarelBeepersInBag world =
    false