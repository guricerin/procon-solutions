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
    let n = read int
    let items = Array.zeroCreate 10
    for i in 0 .. n - 1 do
        let [| a; b; c |] = reada int
        let a, b, c = a - 1, b - 1, c - 1
        items.[a] <- items.[a] + 1
        items.[b] <- items.[b] + 1
        items.[c] <- items.[c] + 1

    let mutable ans = 0
    for i in 0 .. 9 do
        let c = items.[i]
        let c, rem = c / 2, c % 2
        ans <- ans + c
        items.[i] <- rem
    ans <- ans + (Array.sum items) / 4
    puts ans
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
