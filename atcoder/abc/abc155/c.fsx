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

// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------

let main() =
    let n = read int
    let st = SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    for i in 0 .. n - 1 do
        let s = read string
        if st.ContainsKey(s) then st.[s] <- st.[s] + 1 else st.Add(s, 1)
    let cnt = Seq.maxBy (fun (kv: KeyValuePair<string, int>) -> kv.Value) st
    for kv in st do
        if cnt.Value = kv.Value then puts kv.Key
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
