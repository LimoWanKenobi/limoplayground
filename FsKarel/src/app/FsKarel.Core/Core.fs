namespace FsKarel.Core

[<AutoOpen>]
module TypeDefinitions =
  
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
