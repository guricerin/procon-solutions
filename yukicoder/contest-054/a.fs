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
    let A = readChars()
    let B = readChars()
    let ac = Array.zeroCreate 26
    A
    |> Array.iter (fun c ->
        let i = int c - int 'a'
        ac.[i] <- ac.[i] + 1)
    let bc = Array.zeroCreate 26
    B
    |> Array.iter (fun c ->
        let i = int c - int 'a'
        bc.[i] <- bc.[i] + 1)

    if ac = bc then "YES" else "NO"
    |> puts

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
