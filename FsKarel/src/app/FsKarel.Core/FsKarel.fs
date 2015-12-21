namespace FsKarel.Core

module Execution =
    module Actions =
        let private executeIfKarelIsOn action world =
            match Karel.isOn world.karel with
            | false -> Error "Karel is Off."
            | true -> action world
            
        let private getDirection karel =
            match karel.orientation with
              | North -> WallPositions.North
              | South -> WallPositions.South
              | East -> WallPositions.East
              | West -> WallPositions.West
              
        let step' world =
            let direction = getDirection world.karel

            match World.hasWall world.karel.position direction world with
            | true -> Error "Karel has tried to go through a wall."
            | false -> Success { world with karel = Karel.step world.karel }
    
        let turnOff: TurnOff = fun world ->
          Success { world with karel = Karel.turnOff world.karel }
 
        let step :Step = fun world ->
            step' world

        let turnLeft:TurnLeft = fun world ->
          Success { world with karel = Karel.turnLeft world.karel}
         
        let putBeeper: PutBeeper = fun world ->
          Success world
   
        let pickBeeper: PickBeeper = fun world ->
          Success world

    let execute: Execute = fun (program, world) ->
       let result = Success []
       result
