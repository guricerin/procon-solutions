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
    // https://www.kyo-kai.co.jp/img/support/motto/motto6.pdf
    let c = read float
    let [| rin; rout |] = reada float
    let pi = acos -1.0
    let radius = (rout - rin) / 2.
    let area = (pown radius 2) * pi
    let circumference = (radius + rin) * pi * 2.
    let volume = area * circumference
    c * volume |> puts
    ()

main()
writer.Close()
