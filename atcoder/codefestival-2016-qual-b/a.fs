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
    let S = read string
    let T = "CODEFESTIVAL2016"

    seq {
        for i in 0..S.Length - 1 do
            if S.[i] <> T.[i] then
                yield ()
    }
    |> Seq.length
    |> printfn "%d"
    0 // return an integer exit code
