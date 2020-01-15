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
    let f (s: string) = s.Split(',').Length

    let ff s =
        if s = "NONE" then 16 else 16 - (f s)

    let r = read string |> ff
    let g = read string |> ff
    let b = read string |> ff
    r * r * g * g * b * b |> puts
    ()

main()
writer.Close()
