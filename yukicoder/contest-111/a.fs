open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f
    let readChars() = read string |> Seq.toArray
    let readInts() = readChars() |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

[<AutoOpen>]
module Cout =
    let writer = new IO.StreamWriter(new IO.BufferedStream(Console.OpenStandardOutput()))
    let print (s: string) = writer.Write s
    let println (s: string) = writer.WriteLine s
    let inline puts (s: ^a) = string s |> println

let solve() =
    let A = reada int
    let cnt = Array.zeroCreate 13
    for a in A do
        let b = a - 1
        cnt.[b] <- cnt.[b] + 1
    let cnt =
        cnt
        |> Array.sort
        |> Array.rev

    let a, b = cnt.[0], cnt.[1]
    match a, b with
    | 3, 2 -> "FULL HOUSE"
    | 3, _ -> "THREE CARD"
    | 2, 2 -> "TWO PAIR"
    | 2, _ -> "ONE PAIR"
    | _ -> "NO HAND"
    |> puts
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
