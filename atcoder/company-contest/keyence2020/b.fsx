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

let main() =
    let N = read int
    let xls = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let [| x; l |] = reada int64
        xls.[i] <- (x, l)

    // いくつかの区間の中から、重ならないように最大で何個選べるか
    // -> 区間スケジューリング(区間の終端でソートして貪欲)
    let lrs = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let x, d = xls.[i]
        let l, r = x - d, x + d
        lrs.[i] <- (l, r)
    let lrs = lrs |> Array.sortBy (fun (l, r) -> r)

    let mutable nr = Int64.MinValue
    let mutable cnt = 0
    for i in 0 .. N - 1 do
        let l, r = lrs.[i]
        if l < nr then
            ()
        else
            nr <- r
            cnt <- cnt + 1

    puts cnt
    ()

main()
writer.Close()
