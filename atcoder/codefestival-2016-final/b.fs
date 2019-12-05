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

    seq {
        let mutable acc = 0
        for i in 1 .. N do
            acc <- acc + i
            if acc >= N then yield (i, acc)
    }
    |> Seq.head
    |> fun (i, acc) ->
        let y = acc - N
        for j in 1 .. i do
            if j <> y then printfn "%d" j
    0 // return an integer exit code
