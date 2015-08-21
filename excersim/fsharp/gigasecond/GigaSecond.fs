module Gigasecond

open System

type Gigasecond(bornDate: DateTime) =
    let gigaSecond = pown 10 9

    member this.Date = bornDate.AddSeconds(float(gigaSecond))
