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
    let [| H; N |] = reada int
    let hs = Array.zeroCreate (N - 1)
    for i in 0 .. N - 2 do
        hs.[i] <- read int
    let hs =
        Array.append hs [| H |]
        |> Array.sort
        |> Array.rev
    seq {
        for i in 0 .. N - 1 do
            if H = hs.[i] then yield (i + 1)
    }
    |> Seq.head
    |> function
    | n when n % 10 = 1 -> sprintf "%dst" n
    | n when n % 10 = 2 -> sprintf "%dnd" n
    | n when n % 10 = 3 -> sprintf "%drd" n
    | n -> sprintf "%dth" n
    |> puts

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
