open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let [|M1;D1|] = reada int
    let [|M2;D2|] = reada int

    if M1 = 12 then
        if M2 = 1 then printfn "1"
        else printfn "0"
    else
        if M1 < M2 then printfn "1"
        else printfn "0"
    0
