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
    let [| N; M; K |] = reada int

    seq {
        for n in 0 .. N do
            for m in 0 .. M do
                let a = m * (N - n) + n * (M - m)
                if a = K then yield ()
    }
    |> fun s ->
        if Seq.isEmpty s then "No"
        else "Yes"
    |> printfn "%s"
    0 // return an integer exit code
