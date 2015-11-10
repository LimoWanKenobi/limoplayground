namespace FsKarel.Core

type Position = int * int

type Orientation =
    | North
    | South
    | East
    | West
    
type Karel = { position: Position; orientation: Orientation; beepersInBag: int  }

type KarelWorld = { karel: Karel } with
    member this.add() =
        let x,y = this.karel.position 
        x + y
