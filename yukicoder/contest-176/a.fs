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
    let p1 = read int64
    let p2 = read int64
    let n = read int
    let mutable mp = Dictionary<int64, int64>()
    for i in 0 .. n - 1 do
        let r = read int64
        if mp.ContainsKey(r) then mp.[r] <- mp.[r] + 1L else mp.Add(r, 1L)

    seq {
        for kv in mp do
            let k, v = kv.Key, kv.Value
            if v > 1L then yield (p1 + p2) * (v - 1L)
    }
    |> Seq.sum
    |> puts

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
