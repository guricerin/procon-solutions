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
    let [| a; b |] = reada int64
    match a % b with
    | 0L ->
        let mutable s = string (a / b)
        s <- s + "."
        for _ in 1 .. 50 do
            s <- s + "0"
        s
    | _ ->
        let s = string (a / b)
        let mutable s = s + "."
        let mutable rem = a % b
        for _ in 1 .. 50 do
            rem <- (rem % b) * 10L
            let c = rem / b
            s <- s + string c
        s
    |> puts
    ()

main()
writer.Close()
