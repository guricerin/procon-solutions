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

let f =
    function
    | n when n % 15 = 0 -> 8
    | n when n % 3 = 0 -> 4
    | n when n % 5 = 0 -> 4
    | n ->
        n
        |> string
        |> Seq.length


let solve() =
    let a = reada int
    seq {
        for i in 0 .. 4 do
            yield f a.[i]
    }
    |> Seq.sum
    |> puts


[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
