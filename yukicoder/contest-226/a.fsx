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
    let q = read int
    for i in 1 .. q do
        let [| n; k |] = reada int64
        match k with
        | 1L -> n - 1L
        | _ ->
            let b = n * (k - 1L) + 1L |> float
            let k = float k
            Math.Log(b, k) |> int64
        |> puts

    ()

main()
writer.Close()
