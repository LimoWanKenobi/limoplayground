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
    beepersInBag: int
    isOn: bool
}

type WorldState = {
    karel: KarelState
    dimensions: uint32 * uint32;
    walls: Map<Position, WallPositions>
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

type TurnOff = WorldState -> TurnOffResult
type Step = WorldState -> StepResult
type TurnLeft = WorldState -> TurnLeftResult

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

module World =

  let create karel dimensions =
    {
      karel = karel
      dimensions = dimensions
      walls = Map.empty
    }

  let addWall position direction (world:WorldState) =
    let wall =
      match Map.containsKey position world.walls with
      | true -> Map.find position world.walls
      | false -> WallPositions.None

    let newWall = (wall ||| direction)

    { world with walls = Map.add position newWall world.walls }

  let hasWall position direction (world:WorldState) =
    let wall =
      match Map.containsKey position world.walls with
      | true -> Map.find position world.walls
      | false -> WallPositions.None

    direction = (wall &&& direction)

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

    let execute: Execute = fun (program, world) ->
       let result = Success []
       result
