open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let [|a;b|] = reada int
    let f = if (a * b) % 2 = 0 then "Even" else "Odd"
    printfn "%s" f
    0
