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
    let mutable a = (2, 8)
    let mutable b = (3, 9)
    let mutable c = (7, 9)
    for _ in 0 .. n - 1 do
        let [| x1; y1; x2; y2 |] = reada int
        match (x1, y1) with
        | n when n = a -> a <- (x2, y2)
        | n when n = b -> b <- (x2, y2)
        | n when n = c -> c <- (x2, y2)
        | _ -> ()

    match a, b, c with
    | (5, 8), (4, 8), (6, 8) -> "YES"
    | _ -> "NO"
    |> puts
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
