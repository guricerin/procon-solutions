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
    let s =
        read string
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

    s
    |> fun x -> x.Substring(8)
    |> Seq.rev
    |> Seq.map string
    |> String.concat ""
    |> printfn "%s"

    0 // return an integer exit code
