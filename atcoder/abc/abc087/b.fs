open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let a = read int
    let b = read int
    let c = read int
    let x = read int
    seq {
        for i in 0..a do
            for j in 0..b do
                for k in 0..c do
                    yield i * 500 + j * 100 + k * 50
    }
    |> Seq.sumBy (fun y -> if y = x then 1 else 0)
    |> printfn "%d"
    0
