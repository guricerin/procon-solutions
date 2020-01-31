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
    let AB = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let [| a; b |] = reada int
        AB.[i] <- (a, b)

    let x, y =
        AB
        |> Array.maxBy (fun (a, b) -> a)
        |> fun (a, b) -> a, b

    x + y |> printfn "%d"

    0 // return an integer exit code
