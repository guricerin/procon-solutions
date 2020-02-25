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
    let n = readChars()
    let len = n |> Array.length
    let a = Array.zeroCreate len
    let b = Array.zeroCreate len
    for i in 0 .. len - 1 do
        if n.[i] = '7' then
            a.[i] <- '3'
            b.[i] <- '4'
        else
            a.[i] <- '0'
            b.[i] <- n.[i]
    let a =
        a
        |> Array.skipWhile (fun x -> x = '0')
        |> String.Concat

    let b =
        b
        |> Array.skipWhile (fun x -> x = '0')
        |> String.Concat

    sprintf "%s %s" a b |> puts

    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
