namespace FsKarel.Core

type Result =
    | Success
    | Error of string

(* World *)
type Position = int * int

type Orientation =
    | North
    | South
    | East
    | West
    
type KarelState = {
    position: Position
    orientation: Orientation
    beepersInBag: int
}
type WorldState = {
    karel: KarelState
    dimensions: int * int;  
}

(* Actions *)
type Action = 
    | TurnOff
    | Step
    | TurnLeft
    | PickBeeper
    | PutBeeper

type ActionResult = Result * WorldState
type TurnOffResult = ActionResult
type StepResult = ActionResult

type TurnOff = WorldState -> TurnOffResult
type Step = WorldState -> StepResult

type ExecuteAction = WorldState -> ActionResult

(* Program *)
type Instruction = 
    | Action

type Block = Instruction list
type Function = { name: string; instructions: Block }
type Program = { main: Function; functions: Function list }

(* Program Execution *)
type ProgramExecution = Result * ActionResult list
    
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
               
            (Success, newWorld)
        
        
    let execute: Execute = fun (program, world) ->
       let result = (Success, [])
       result
        
