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
    let A = reada int
    let st = Dictionary<int, int>()
    for i in 0 .. n - 1 do
        let a = A.[i]
        if st.ContainsKey(a) then st.[a] <- st.[a] + 1 else st.Add(a, 1)
    seq {
        for kv in st do
            let v = kv.Value
            if v = 1 then yield ()
    }
    |> Seq.length
    |> puts

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
