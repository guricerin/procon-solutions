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
    let s = read string

    let l = s.Substring(0, 4)
    let r = s.Substring(4)
    sprintf "%s %s" l r |> printfn "%s"
    0 // return an integer exit code
