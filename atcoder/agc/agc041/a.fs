open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

let writer = new IO.StreamWriter(new IO.BufferedStream(Console.OpenStandardOutput()))
let print (s: string) = writer.Write s
let println (s: string) = writer.WriteLine s

[<AutoOpen>]
module Util =
    let strRev (s: string): string =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

    let inline roundup (x: ^a) (y: ^a): ^a =
        let one = LanguagePrimitives.GenericOne
        (x + y - one) / y

    let inline sameParity (x: ^a) (y: ^a): bool =
        let one = LanguagePrimitives.GenericOne
        (x &&& one) = (y &&& one)

let solve() =
    let [|n;a;b|] = reada int64
    match sameParity a b with
    | true ->
        let ans = (b - a) / 2L
        println (string ans)
    | false ->
        let la = a - 1L
        let ans1 = (b - a - 1L) / 2L + la + 1L
        let rb = n - b
        let ans2 = (b - a - 1L) / 2L + rb + 1L
        let ans = min ans1 ans2
        println (string ans)

    ()

[<EntryPoint>]
let main _ =
    solve()
    writer.Close()
    0 // return an integer exit code
