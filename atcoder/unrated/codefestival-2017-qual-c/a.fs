open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

module Util =
    let strRev s =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

[<EntryPoint>]
let main _ =
    let s = read string

    seq {
        for i in 0 .. s.Length - 2 do
            if s.[i] = 'A' && s.[i + 1] = 'C' then yield ()
    }
    |> fun x ->
        if Seq.isEmpty x then "No"
        else "Yes"
    |> printfn "%s"
    0 // return an integer exit code
