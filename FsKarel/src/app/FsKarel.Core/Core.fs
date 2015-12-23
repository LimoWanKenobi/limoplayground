namespace FsKarel.Core

[<AutoOpen>]
module TypeDefinitions =
    (* Railway oriented programming stuff.
     From http://fsharpforfunandprofit.com/posts/recipe-part2/  *)
    // the two-track type
    type Result<'TSuccess,'TFailure> = 
        | Success of 'TSuccess
        | Failure of 'TFailure

    // convert a single value into a two-track result
    let succeed x = 
        Success x

    // convert a single value into a two-track result
    let fail x = 
        Failure x

    // apply either a success function or failure function
    let either successFunc failureFunc twoTrackInput =
        match twoTrackInput with
        | Success s -> successFunc s
        | Failure f -> failureFunc f

    // convert a switch function into a two-track function
    let bind f = 
        either f fail

    // pipe a two-track value into a switch function 
    let (>>=) x f = 
        bind f x

    // compose two switches into another switch
    let (>=>) s1 s2 = 
        s1 >> bind s2

    // convert a one-track function into a switch
    let switch f = 
        f >> succeed

    // convert a one-track function into a two-track function
    let map f = 
        either (f >> succeed) fail

    // convert a dead-end function into a one-track function
    let tee f x = 
        f x; x 

    // convert a one-track function into a switch with exception handling
    let tryCatch f exnHandler x =
        try
            f x |> succeed
        with
        | ex -> exnHandler ex |> fail

    // convert two one-track functions into a two-track function
    let doubleMap successFunc failureFunc =
        either (successFunc >> succeed) (failureFunc >> fail)

    // add two switches in parallel
    let plus addSuccess addFailure switch1 switch2 x = 
        match (switch1 x),(switch2 x) with
        | Success s1,Success s2 -> Success (addSuccess s1 s2)
        | Failure f1,Success _  -> Failure f1
        | Success _ ,Failure f2 -> Failure f2
        | Failure f1,Failure f2 -> Failure (addFailure f1 f2)
    
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
    
    type ActionResult = Result<WorldState, string>
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
    type ProgramExecution = Result<ActionResult list, string>
    
    type Execute = Program * WorldState -> ProgramExecution
