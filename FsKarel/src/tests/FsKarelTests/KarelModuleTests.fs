module FsKarel.Core.KarelModuleTests

open NUnit.Framework
open FsUnit
open FsKarel.Core

let fail() = true |> should be False
let success() = true |> should be True

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
  let karel = Karel.create (0u, 0u) Orientation.South 5u
  
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
  let karel = Karel.create (0u, 0u) Orientation.South 2u
  
  match Karel.removeBeeperFromBag karel with
  | Error _ -> fail()
  | Success k ->
    k.beepersInBag |> should equal 1u
    match Karel.removeBeeperFromBag k with
    | Error _ -> fail()
    | Success k -> 
      k.beepersInBag |> should equal 0u
      match Karel.removeBeeperFromBag k with
      | Error _ -> success()
      | Success k -> fail()
  
[<Test>]
let ``turnOff should turn off karel``() =
  let karel = Karel.create (0u, 0u) Orientation.South 2u
  Karel.isOn karel |> should be True
  
  let karel = Karel.turnOff karel
  Karel.isOn karel |> should be False
  
  