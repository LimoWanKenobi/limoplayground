namespace FsKarel.Core

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
    
  let setBeepersInBag b (karel :KarelState) =
    karel
    
  let addBeeperToBag (karel :KarelState) =
    karel
    
  let removeBeeperFromBag (karel :KarelState) : Result<KarelState> =
    Success karel

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
    karel