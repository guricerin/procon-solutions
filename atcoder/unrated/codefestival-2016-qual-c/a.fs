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

    seq {
        for i in 0 .. S.Length - 1 do
            if S.[i] = 'C' then
                for j in i + 1 .. S.Length - 1 do
                    if S.[j] = 'F' then yield ()
    }
    |> Seq.length
    |> fun x ->
        if x > 0 then "Yes"
        else "No"
    |> printfn "%s"
    0 // return an integer exit code
