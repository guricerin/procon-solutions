open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let n = read int
    let ass = Array.zeroCreate n
    for i in 0..n-1 do ass.[i] <- read int

    ass
    |> Array.distinct
    |> Array.length
    |> printfn "%d"
    0
