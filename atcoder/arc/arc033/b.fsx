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
    let [| na; nb |] = reada int
    let A = reada int
    let B = reada int
    let memo = Dictionary<int, int>()
    for a in A do
        if memo.ContainsKey(a) then () else memo.Add(a, 1)
    for b in B do
        if memo.ContainsKey(b) then memo.[b] <- memo.[b] + 1 else memo.Add(b, 1)

    let mutable (numer, denom) = (0, 0)
    for m in memo do
        let k, v = m.Key, m.Value
        if v > 1 then numer <- numer + 1
        denom <- denom + 1

    float numer / float denom |> puts

    ()

main()
writer.Close()
