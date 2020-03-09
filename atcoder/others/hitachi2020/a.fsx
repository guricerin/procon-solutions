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
    let len = s.Length
    let mutable ok = true
    for i in 0 .. 2 .. len - 2 do
        if not (s.[i] = 'h' && s.[i + 1] = 'i') then ok <- false
    if len % 2 = 1 then ok <- false
    if ok then "Yes" else "No"
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
