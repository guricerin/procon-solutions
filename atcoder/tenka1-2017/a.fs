open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

[<EntryPoint>]
let main _ =
    let s = readInts()

    s
    |> Array.filter (fun x -> x = 1)
    |> Array.length
    |> printfn "%d"
    0 // return an integer exit code
