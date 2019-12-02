open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let N = read int
    let X = float N / 1.08 |> fun x -> Math.Ceiling(x) |> int
    let M = float X * 1.08 |> int
    if N <> M then printfn ":("
    else printfn "%d" X
    0
