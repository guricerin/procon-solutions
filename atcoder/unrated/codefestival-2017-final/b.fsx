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
    let s = readChars()
    let xs = Array.zeroCreate 3
    for c in s do
        let c = int c - int 'a'
        xs.[c] <- xs.[c] + 1

    // 実験してみたら、各文字の出現回数の差が1以内ならokとわかる
    let xs = xs |> Array.sort
    let mutable ok = true
    for i in 0 .. 2 do
        if abs (xs.[0] - xs.[i]) >= 2 then ok <- false

    if ok then "YES" else "NO"
    |> puts

    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
