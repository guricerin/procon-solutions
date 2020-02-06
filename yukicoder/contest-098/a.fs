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
    let xys = Array.zeroCreate n
    for i in 0 .. n - 1 do
        let [| x; y |] = reada int
        xys.[i] <- (x, y)

    let d =
        let (x, y) = xys.[0]
        y - x

    let ok = xys |> Array.forall (fun (x, y) -> y - x = d)
    match ok, d with
    | true, d when d > 0 -> d
    | _ -> -1
    |> puts

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
