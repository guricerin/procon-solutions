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
    let len = min s.Length 4
    let s = s.Substring(0, len)

    if s = "YAKI" then "Yes"
    else "No"
    |> printfn "%s"

    0 // return an integer exit code
