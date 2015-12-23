namespace FsKarel.Core

module World =

    let create karel dimensions =
        {
            karel = karel
            dimensions = dimensions
            walls = Map.empty
            beepers = Map.empty
        }
    
    let Default = create Karel.Default (100u, 100u)
  
    let private ifPositionIsInWorld action position world =
        let w, h = world.dimensions
        match position with
            | x, y when x > w || y > h -> Failure "Position is outside of the world."
            | _ -> Success (action position world) 
  
    let addWall direction position world =
        let addWall' direction position (world:WorldState) =
            let wall = if Map.containsKey position world.walls
                       then Map.find position world.walls
                       else WallPositions.None

            let newWall = (wall ||| direction)

            { world with walls = Map.add position newWall world.walls }
        
        ifPositionIsInWorld (addWall' direction) position world
        
    let private getWallsFor position world =
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

        wallsPosition ||| wallWest ||| wallEast ||| wallSouth ||| wallNorth

    let hasWall position direction world =
        let walls = getWallsFor position world
        direction = (walls &&& direction)
    
    let setBeepers amount position world =
        let setBepers' amount position world = 
            { world with beepers = Map.add position amount world.beepers }
        
        ifPositionIsInWorld (setBepers' amount) position world
        
    let putBeeper position (world:WorldState) =
        let putBeeper' position (world:WorldState) =
            world
    
        ifPositionIsInWorld putBeeper' position world
        
    let pickBeeper position (world:WorldState) =
        let pickBeeper' position (world:WorldState) =
            world
    
        ifPositionIsInWorld pickBeeper' position world
        
    let beepersAtPosition position (world:WorldState) = 
        let beepersAtPosition' position (world:WorldState) = 
            match Map.containsKey position world.beepers with
            | true -> Map.find position world.beepers
            | false -> 0u
        
        ifPositionIsInWorld beepersAtPosition' position world
        
    let hasBeepers position (world:WorldState) =
        match beepersAtPosition position world with
        | Failure e -> Failure e
        | Success beepers -> Success (beepers > 0u)  
        
    let hasKarelBeepersInBag world =
        false