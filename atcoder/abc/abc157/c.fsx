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
    let [| N; M |] = reada int
    let scs = Array.zeroCreate 5
    for i in 0 .. M - 1 do
        let [| s; c |] = reada int
        let s = s - 1
        scs.[i] <- s, c

    let mutable ans = "-1"
    for i in 1000 .. -1 .. 0 do
        let cand = string i
        let mutable ok = true
        if cand.Length <> N then
            ok <- false
        else
            for m in 0 .. M - 1 do
                let s, c = scs.[m]
                let b = Convert.ToInt32(string cand.[s])
                if b <> c then ok <- false
        if ok then ans <- cand

    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
