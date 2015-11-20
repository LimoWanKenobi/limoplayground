namespace FsKarel.Core

module Execution =
    module Actions =

        let step :Step = fun world ->
            let direction =
              match world.karel.orientation with
              | North -> WallPositions.North
              | South -> WallPositions.South
              | East -> WallPositions.East
              | West -> WallPositions.West

            match World.hasWall world.karel.position direction world with
            | true -> Error "Karel has tried to go through a wall."
            | false ->
              Success { world with karel = Karel.step world.karel }

        let turnLeft:TurnLeft = fun world ->
          let karel = world.karel

          let newOrient =
            match karel.orientation with
            | North -> West
            | West -> South
            | South -> East
            | East -> North

          Success { world with karel = { karel with orientation = newOrient }}

        let turnOff: TurnOff = fun world ->
          let karel = world.karel

          match karel.isOn with
          | true -> Success { world with karel = { karel with isOn = false }}
          | false -> Success world
          
        let putBeeper: PutBeeper = fun world ->
          Success world
   
        let pickBeeper: PickBeeper = fun world ->
          Success world

    let execute: Execute = fun (program, world) ->
       let result = Success []
       result
