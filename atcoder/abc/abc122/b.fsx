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

let isok c = c = 'A' || c = 'G' || c = 'C' || c = 'T'

let main() =
    let s = readChars()
    let mutable ans = 0
    let mutable l = 0
    let len = Array.length s
    while l < len do
        let mutable r = l
        while r < len && isok s.[r] do
            r <- r + 1

        let tmp = r - l
        ans <- max ans tmp
        l <- r + 1
    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
