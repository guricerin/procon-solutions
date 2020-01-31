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
    let [| H; W |] = reada int
    let S = Array.zeroCreate H
    for y in 0 .. H - 1 do
        S.[y] <- reada string

    for y in 0 .. H - 1 do
        for x in 0 .. W - 1 do
            if S.[y].[x] = "snuke" then
                let a = sprintf "%c%d" (48 + x |> fun b -> Convert.ToChar(b)) y
                printfn "%s" a

    0 // return an integer exit code
