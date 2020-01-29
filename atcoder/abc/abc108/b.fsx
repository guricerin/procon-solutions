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
    let [| x1; y1; x2; y2 |] = reada int
    // 図を書け
    let a, b = x2 - x1, y2 - y1
    let x3, y3 = x2 - b, y2 + a
    let x4, y4 = x1 - b, y1 + a
    sprintf "%d %d %d %d" x3 y3 x4 y4 |> puts
    ()

main()
writer.Close()
