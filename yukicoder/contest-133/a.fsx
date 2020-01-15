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

let main() =
    let n = read int
    let st = Dictionary<string, int>()
    for i in 1 .. n do
        let s = read string
        if st.ContainsKey(s) then st.[s] <- st.[s] + 1 else st.Add(s, 1)
    let mult =
        st
        |> Seq.maxBy (fun kv -> kv.Value)
        |> fun kv -> kv.Value
    if (mult - 1) * 2 >= n then "NO" else "YES"
    |> puts
    ()

main()
writer.Close()
