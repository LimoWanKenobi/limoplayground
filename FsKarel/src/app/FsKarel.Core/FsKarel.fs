namespace FsKarel.Core

(* World *)
type Position = int * int

type Orientation =
    | North
    | South
    | East
    | West
    
type KarelState = { position: Position; orientation: Orientation; beepersInBag: int  }
type WorldState = { karel: KarelState; dimensions: int * int;  }

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

        let step :Step = fun world ->
            let karel = world.karel
            let x,y = karel.position
            
            let newPos = 
                match karel.orientation with
                | North -> (x, y+1) 
                | South -> (x, y-1)
                | East -> (x+1, y)
                | West -> (x-1, y)
                 
            let newWorld = { world with karel = { karel with position = newPos } }
               
            { original = world; result = newWorld }
        
        
    let execute: Execute = fun (program, world) ->
       let result = { executedSteps = [] }
       let error_result = { message = "Not implemented yet"; executedSteps = [] }
       Success result
        
