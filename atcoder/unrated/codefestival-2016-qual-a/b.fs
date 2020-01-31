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
    let N = read int
    let A = reada int |> Array.map (fun x -> x - 1)
    let B = A |> Array.indexed

    B
    |> Array.filter (fun (i, a) -> i = A.[a] && a = A.[i])
    |> Array.length
    |> fun x -> x / 2
    |> printfn "%d"
    0 // return an integer exit code
