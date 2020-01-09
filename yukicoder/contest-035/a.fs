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
    let N = read int
    let tss = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let [| t; s |] = reada string
        let t = Convert.ToInt32(t)
        let s = Seq.length s
        tss.[i] <- (t, s)

    let lim m = 12 * m / 1000
    let mutable ok = 0
    for ts in tss do
        let t, s = ts
        let l = lim t
        if l >= s then ok <- ok + s else ok <- ok + l

    let totalS = tss |> Array.fold (fun acc (t, s) -> acc + s) 0
    let ans = sprintf "%d %d" ok (totalS - ok)
    puts ans
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
