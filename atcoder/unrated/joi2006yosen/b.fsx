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
    let memo = Dictionary<char, char>()
    for i in 0 .. n - 1 do
        let [| a; b |] = reada char
        memo.Add(a, b)

    let m = read int
    let mutable ans = ""
    for i in 0 .. m - 1 do
        let c = read char

        let c =
            if memo.ContainsKey(c) then memo.[c] else c
        ans <- sprintf "%s%c" ans c

    puts ans
    ()

main()
writer.Close()
