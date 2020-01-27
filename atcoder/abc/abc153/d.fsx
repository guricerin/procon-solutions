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
    let h = read int64

    // 2^n <= h <= 2^(n+1) - 1 にあるとき、答えは 2^(n+1) - 1
    let mutable (ok, ng) = (64, -1)
    while abs (ok - ng) > 1 do
        let mid = (ok + ng) / 2
        let a = pown 2L mid
        if h < a then ok <- mid else ng <- mid

    let ans = (pown 2L ok) - 1L
    ans |> puts
    ()

main()
writer.Close()
