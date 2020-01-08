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

let solve() =
    let x = read int64
    let y = read int64
    let t = read int64

    let mutable ans =
        match y,x with
        | _ when y >= 0L && x > 0L -> 1L
        | _ when y >= 0L && x = 0L -> 0L
        | _ when y >= 0L && x < 0L -> 1L
        | _ -> 2L

    let y = abs y
    let x = abs x
    ans <- ans + (x + t - 1L) / t
    ans <- ans + (y + t - 1L) / t
    puts ans
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
