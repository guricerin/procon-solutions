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
    let [| N; K; S |] = reada int
    let S = int64 S

    let lim = int64 1e9

    let gomi =
        if S <> lim then S + 1L else 1L

    // l <= r なので、適当にSをK個並べる。残りは適当になにか並べる。
    let ass = ResizeArray<int64>()
    for i in 1 .. K do
        ass.Add(S)
    for i in 1 .. N - K do
        ass.Add(gomi)

    String.Join(" ", ass) |> puts

    ()

main()
writer.Close()
