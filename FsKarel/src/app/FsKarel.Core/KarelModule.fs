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
        
    let hasBeepersInBag karel =
        karel.beepersInBag > 0u
        
    let isOn karel =
        karel.isOn
        
    let setBeepersInBag beepers karel =
        { karel with beepersInBag = beepers }
        
    let addBeeperToBag karel =
        { karel with beepersInBag = karel.beepersInBag + 1u }
        
    let removeBeeperFromBag karel =
        match karel.beepersInBag with
        | 0u -> Failure "Karel has no beepers in its bag."
        | bp -> Success { karel with beepersInBag = bp - 1u }
    
    let turnOff karel =
        { karel with isOn = false }

    let step karel = 
        let x,y = karel.position
        let newPos =
            match karel.orientation with
            | North -> (x, y + 1u)
            | South -> (x, y - 1u)
            | East  -> (x + 1u, y)
            | West  -> (x - 1u, y)
        
        { karel with position = newPos }
        
    let turnLeft karel =
        let newOrientation =
            match karel.orientation with
            | North -> West
            | West -> South
            | South -> East
            | East -> North
            
        { karel with orientation = newOrientation }