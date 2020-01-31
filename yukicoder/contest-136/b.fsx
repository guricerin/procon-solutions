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
    let [| n; k |] = reada int
    let A = reada int

    let mutable ans = 0
    // (i, i+k)をバブルソート
    for i in 0 .. n - 1 do
        for j in i .. k .. n - 1 do
            if A.[i] > A.[j] then
                let tmp = A.[i]
                A.[i] <- A.[j]
                A.[j] <- tmp
                ans <- ans + 1

    seq {
        for i in 0 .. n - 2 do
            if A.[i] > A.[i + 1] then yield ()
    }
    |> Seq.isEmpty
    |> fun x ->
        if x then ans else -1
    |> puts
    ()

main()
writer.Close()
