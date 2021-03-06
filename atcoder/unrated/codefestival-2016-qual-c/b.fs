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
    let [| K; T |] = reada int
    let A = reada int

    let a = A |> Array.max
    let ans = (a - 1) - (K - a)
    List.max [ 0; ans ] |> printfn "%d"
    0 // return an integer exit code
