namespace FsKarel.Core

module Karel =

  let create position orientation beepersInBag =
    {
        position = position
        orientation = orientation
        beepersInBag = beepersInBag
        isOn = true
    }
    
  let Default = create (0u, 0u) Orientation.East 0u
    
  let hasBeepersInBag (karel :KarelState) =
    false
    
  let isOn (karel :KarelState) =
    karel.isOn
    
  let setBeepersInBag beepers (karel :KarelState) =
    karel
    
  let addBeeperToBag (karel :KarelState) =
    karel
    
  let removeBeeperFromBag (karel :KarelState) : Result<KarelState> =
    Success karel
  
  let turnOff (karel :KarelState) =
    if karel.isOn then { karel with isOn = false } else karel

  let step (karel :KarelState) = 
    (*let x,y = karel.position
    let newPos =
      match karel.orientation with
      | North -> (x, y + 1u)
      | South -> (x, y - 1u)
      | East  -> (x + 1u, y)
      | West  -> (x - 1u, y)
      
    { karel with position = newPos }*)
    karel
    
  let turnLeft (karel :KarelState) =
    (*
    let newOrient =
            match karel.orientation with
            | North -> West
            | West -> South
            | South -> East
            | East -> North*)
    karel