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
    let n = read int
    let xs = reada int64 |> Array.sort

    let ok1 = Array.tail xs |> Array.forall (fun x -> x <> xs.[0])
    let mutable ok2 = true
    for i in 0 .. n - 3 do
        let d1 = xs.[i] - xs.[i + 1] |> abs
        let d2 = xs.[i + 1] - xs.[i + 2] |> abs
        if d1 <> d2 then ok2 <- false

    if ok1 && ok2 then "YES" else "NO"
    |> puts

    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
