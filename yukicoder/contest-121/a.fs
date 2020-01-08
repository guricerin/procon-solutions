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
    let s = read string
    let mutable mp = Dictionary<char, int>()
    for c in s do
        if c = 't' || c = 'r' || c = 'e' then
            if mp.ContainsKey(c) then mp.[c] <- mp.[c] + 1 else mp.Add(c, 1)

    try
        let t, r, e = mp.['t'], mp.['r'], mp.['e'] / 2
        let ans = List.min [ t; r; e ]
        ans |> puts
    with e -> puts 0

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
