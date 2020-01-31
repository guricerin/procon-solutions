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
    let [| m; n |] = reada int
    let m = float m

    // 1回の期待値が
    // (m * 2 / 3) + (m + 1) / 3 + 0 / 3 -> m + 1/3
    // これをn回繰り返す
    let mutable ans = m
    for i in 0 .. n - 1 do
        ans <- ans + 1.0 / 3.0

    puts ans
    ()

main()
writer.Close()
