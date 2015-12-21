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
    karel.beepersInBag > 0u
    
  let isOn (karel :KarelState) =
    karel.isOn
    
  let setBeepersInBag beepers (karel :KarelState) =
    { karel with beepersInBag = beepers }
    
  let addBeeperToBag (karel :KarelState) =
    { karel with beepersInBag = karel.beepersInBag + 1u }
    
  let removeBeeperFromBag (karel :KarelState) : Result<KarelState> =
    match karel.beepersInBag with
    | 0u -> Error "Karel has no beepers in its bag."
    | _ -> Success { karel with beepersInBag = karel.beepersInBag - 1u }
  
  let turnOff (karel :KarelState) =
    if karel.isOn then { karel with isOn = false } else karel

  let step (karel :KarelState) = 
    let x,y = karel.position
    let newPos =
      match karel.orientation with
      | North -> (x, y + 1u)
      | South -> (x, y - 1u)
      | East  -> (x + 1u, y)
      | West  -> (x - 1u, y)
      
    { karel with position = newPos }
    
  let turnLeft (karel :KarelState) =
    let newOrientation =
            match karel.orientation with
            | North -> West
            | West -> South
            | South -> East
            | East -> North
    { karel with orientation = newOrientation }