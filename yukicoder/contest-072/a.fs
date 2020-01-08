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

let ok i j k = i <> j && j <> k && i <> k

let ok2 i j k =
    let l = [ i; j; k ] |> List.sort
    List.min l = j || List.max l = j

let solve() =
    let n = read int
    let A = reada int
    let mutable cnt = 0
    for i in 0 .. n - 3 do
        let a, b, c = A.[i], A.[i + 1], A.[i + 2]
        if (ok a b c) && (ok2 a b c) then cnt <- cnt + 1

    puts cnt


[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
