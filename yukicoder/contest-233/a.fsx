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
    let x = read int

    let ls =
        if x <> 10 then [ for i in x .. -1 .. 1 -> string i ] else [ for i in 9 .. -1 .. 0 -> string i ]

    for i in 1 .. x do
        let s = String.Join("", ls)
        print s
    puts ""
    ()

main()
writer.Close()
