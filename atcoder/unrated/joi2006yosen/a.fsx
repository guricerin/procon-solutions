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
    let n = read int
    let mutable ap, bp = 0L, 0L
    for i in 0 .. n - 1 do
        let [| a; b |] = reada int64
        if a = b then
            ap <- ap + a
            bp <- bp + b
        elif a < b then
            bp <- bp + a + b
        else
            ap <- ap + a + b

    sprintf "%d %d" ap bp |> puts
    ()

main()
writer.Close()
