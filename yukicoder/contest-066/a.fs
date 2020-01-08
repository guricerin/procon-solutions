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

let left i = i * 2
let right i = i * 2 + 1

let loop s =
    let lim = Array.length s

    let rec go j acc =
        if j >= lim then
            acc
        else
            match s.[j] with
            | 'L' -> go (j + 1) (left acc)
            | _ -> go (j + 1) (right acc)

    go 0 1

let solve() =
    let s = readChars()
    loop s |> puts

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
