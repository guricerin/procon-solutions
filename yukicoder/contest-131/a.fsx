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

open System.Text.RegularExpressions

let main() =
    let s = read string
    let reg = Regex("…+")
    reg.Matches(s)
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Length)
    |> fun x ->
        if Seq.isEmpty x then 0 else Seq.max x
    |> puts

    ()

main()
writer.Close()
