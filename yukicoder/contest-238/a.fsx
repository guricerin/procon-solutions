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
    for i in 0 .. Array.length s - 2 do
        if s.[i] = 'a' && s.[i + 1] = 'o' then
            s.[i] <- 'k'
            s.[i + 1] <- 'i'
    String.Concat s |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
