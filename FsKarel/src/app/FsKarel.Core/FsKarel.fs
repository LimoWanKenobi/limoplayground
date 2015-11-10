namespace FsKarel.Core

(* World *)
type Position = int * int

type Orientation =
    | North
    | South
    | East
    | West
    
type KarelState = { position: Position; orientation: Orientation; beepersInBag: int  }
type WorldState = { karel: KarelState }

(* Actions *)
type Action = 
    | TurnOff
    | Step
    | TurnLeft
    | PickBeeper
    | PutBeeper

type ActionResult = { original: WorldState; result: WorldState }
type TurnOffResult = ActionResult
type StepResult = ActionResult

type Step = WorldState -> StepResult

type ExecuteAction = WorldState -> ActionResult

(* Program *)
type Instruction = 
    | Action

type Block = Instruction list
type Function = { name: string; instructions: Block }
type Program = { main: Function; functions: Function list }

(* Program Execution *)
type ExecutionError = { message: string; executedSteps: ActionResult list }
type ExecutionResult = { executedSteps: ActionResult list }
type ProgramExecution = 
    | Success of ExecutionResult
    | Error of ExecutionError
    
type Execute = Program * WorldState -> ProgramExecution

module Execution =
    module Actions =
        let todo() = ()

        let step :Step = fun world -> { original = world; result = world }
        
        
    let execute: Execute = fun (program, world) ->
       let result = { executedSteps = [ Actions.step world ] }
       Success result
        
