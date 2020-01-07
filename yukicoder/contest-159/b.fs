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

let countof (target: string) (s: string) =
    let mutable res = 0
    let mutable idx = target.IndexOf(s, 0)
    while idx <> -1 do
        res <- res + 1
        idx <- target.IndexOf(s, idx + s.Length)
    res


let solve() =
    let s = read string
    sprintf "%d %d" (countof s "(^^*)") (countof s "(*^^)") |> puts

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
