module Bob

open System

type Bob(message) = 

    let hasLetters (str: string) =
        str
        |> String.exists Char.IsLetter

    let (|IsYell|_|) (str: string) =
        if hasLetters str && str.ToUpper() = str then Some(IsYell) else None

    let (|IsQuestion|_|) (str: string) =
        if str.EndsWith("?") then Some(IsQuestion) else None

    let (|IsSilence|_|) (str: string) =
        if String.IsNullOrWhiteSpace(str) then Some(IsSilence) else None

    member this.hey() =
        match message with
        | IsYell -> "Whoa, chill out!"
        | IsQuestion -> "Sure."
        | IsSilence -> "Fine. Be that way!"
        | _ -> "Whatever."