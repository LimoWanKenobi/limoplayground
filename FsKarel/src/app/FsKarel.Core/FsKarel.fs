namespace FsKarel.Core

type Result<'T> =
    | Success of 'T
    | Error of string

(* World *)
type Position = uint32 * uint32

type Orientation =
    | North
    | South
    | East
    | West

type WallPositions =
  | None  = 0b0000
  | North = 0b0001
  | South = 0b0010
  | East  = 0b0100
  | West  = 0b1000

type KarelState = {
    position: Position
    orientation: Orientation
    beepersInBag: uint32
    isOn: bool
}

type WorldState = {
    karel: KarelState
    dimensions: uint32 * uint32;
    walls: Map<Position, WallPositions>
    beepers: Map<Position, uint32>
}

(* Actions *)
type Action =
    | TurnOff
    | Step
    | TurnLeft
    | PickBeeper
    | PutBeeper

type ActionResult = Result<WorldState>
type TurnOffResult = ActionResult
type StepResult = ActionResult
type TurnLeftResult = ActionResult
type PickBeeperResult = ActionResult
type PutBeeperResult = ActionResult

type TurnOff = WorldState -> TurnOffResult
type Step = WorldState -> StepResult
type TurnLeft = WorldState -> TurnLeftResult
type PickBeeper = WorldState -> PickBeeperResult
type PutBeeper = WorldState -> PutBeeperResult

type ExecuteAction = WorldState -> ActionResult

(* Program *)
type Instruction =
    | Action

type Block = Instruction list
type Function = { name: string; instructions: Block }
type Program = { main: Function; functions: Function list }

(* Program Execution *)
type ProgramExecution = Result<ActionResult list>

type Execute = Program * WorldState -> ProgramExecution

module Karel =

  let create position orientation beepersInBag =
    {
        position = position
        orientation = orientation
        beepersInBag = beepersInBag
        isOn = true
    }
    
  let hasBeepersInBag (karel :KarelState) =
    false
    
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

module Execution =
    module Actions =

        let step :Step = fun world ->
            let karel = world.karel
            let x,y = karel.position

            let direction =
              match karel.orientation with
              | North -> WallPositions.North
              | South -> WallPositions.South
              | East -> WallPositions.East
              | West -> WallPositions.West

            match World.hasWall karel.position direction world with
            | true -> Error "Karel has tried to go through a wall."
            | false ->
              let newPos =
                  match karel.orientation with
                  | North -> (x, y + 1u)
                  | South -> (x, y - 1u)
                  | East  -> (x + 1u, y)
                  | West  -> (x - 1u, y)

              Success { world with karel = { karel with position = newPos } }

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
