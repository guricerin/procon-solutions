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
    let N = read int
    let ss = Array.zeroCreate N
    let ts = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let [| s; t |] = reada string
        let t = Convert.ToInt32(t)
        ss.[i] <- s
        ts.[i] <- t
    let X = read string
    let mutable idx = 0
    Array.iteri (fun i s ->
        if s = X then idx <- i) ss
    let idx = idx + 1
    match idx > N - 1 with
    | true -> 0
    | _ -> Array.sum ts.[idx..]
    |> puts

    ()

main()
writer.Close()
