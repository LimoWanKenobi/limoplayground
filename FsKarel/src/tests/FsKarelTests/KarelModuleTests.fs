module FsKarel.Core.KarelModuleTests

open NUnit.Framework
open FsUnit
open FsKarel.Core

let testFail() = true |> should be False
let testSuccess() = true |> should be True

[<Test>]
let ``Creation of new karel should work``() =
  let position = (5u, 6u)
  let orientation = Orientation.South
  let beepersInBag = 7u
  
  let karel = Karel.create position orientation beepersInBag
  
  karel.position |> should equal position
  karel.orientation |> should equal orientation
  karel.beepersInBag |> should equal beepersInBag
  karel.isOn |> should be True
  
[<Test>]
let ``HasBeepersInBag should be false when there are no beepers in bag``() =
  let karel = Karel.Default
  
  Karel.hasBeepersInBag karel |> should be False
  
[<Test>]
let ``HasBeepersInBag should be true when there are beepers in bag``() =
  let karel = { Karel.Default with beepersInBag = 5u }
  
  Karel.hasBeepersInBag karel |> should be True
  
[<Test>]
let ``SetBeepersInBag should be set the correct value``() =
  let karel = Karel.Default
  
  let beepers = 10u
  let karel = Karel.setBeepersInBag beepers karel
  karel.beepersInBag |> should equal beepers
  
  let beepers = 25u
  let karel = Karel.setBeepersInBag beepers karel
  karel.beepersInBag |> should equal beepers
  
[<Test>]
let ``Adding beepers to the bag tests``() = 
  let karel = Karel.Default
  
  let karel = Karel.addBeeperToBag karel
  karel.beepersInBag |> should equal 1u
  
  let karel = Karel.addBeeperToBag karel
  karel.beepersInBag |> should equal 2u
  
[<Test>]
let ``Removing beepers from the bag tests``() = 
  let karel = { Karel.Default with beepersInBag = 2u }
  
  let assertRemoveBeeper expected karel = 
    karel.beepersInBag |> should equal expected
    karel
    
  let testFn = Karel.removeBeeperFromBag
              >> map (assertRemoveBeeper 1u)
              >> bind Karel.removeBeeperFromBag
              >> map (assertRemoveBeeper 0u)
              >> bind Karel.removeBeeperFromBag
              >> either (fun _ -> testFail()) (fun _ -> testSuccess())
              
  testFn karel
  
[<Test>]
let ``turnOff should turn off karel``() =
  let karel = Karel.Default
  Karel.isOn karel |> should be True
  
  let karel = Karel.turnOff karel
  Karel.isOn karel |> should be False
  
let assertTurnLeft karel expectedOrientation =
  karel.orientation |> should not' (equal expectedOrientation)
  let result = Karel.turnLeft karel
  result.orientation |> should equal expectedOrientation
  result
    
[<Test>]
let ``turnLeft should turn karel to the left``() =
  let karel = Karel.Default

  let karel = assertTurnLeft karel North
  let karel = assertTurnLeft karel West
  let karel = assertTurnLeft karel South
  ()

[<Test>]
let ``Step should increment x when moving to the east``() =
  let karel = Karel.create (1u, 2u) East 0u
  let result = Karel.step karel

  result.position |> should equal (2u, 2u)

[<Test>]
let ``Step should increment y when moving to the north``() =
  let karel = Karel.create (1u, 2u) North 0u
  let result = Karel.step karel

  result.position |> should equal (1u, 3u)

[<Test>]
let ``Step should decrement y when moving to the south``() =
  let karel = Karel.create (1u, 2u) South 0u
  let result = Karel.step karel

  result.position |> should equal (1u, 1u)

[<Test>]
let ``Step should decrement w when moving to the west``() =
  let karel = Karel.create (1u, 2u) West 0u
  let result = Karel.step karel

  result.position |> should equal (0u, 2u)