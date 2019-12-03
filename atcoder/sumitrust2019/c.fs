open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let x = read int
    let y = x / 100

    if 100 * y <= x && x <= 105 * y then printfn "1"
    else printfn "0"
    0 // return an integer exit code
