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
    let A = reada int64 |> Array.sort
    let B = reada int64 |> Array.sort
    let C = reada int64 |> Array.sort

    let lower (arr: int64 array) n a =
        let mutable (ok, ng) = (n, -1)
        while abs (ok - ng) > 1 do
            let mid = (ok + ng) / 2
            if a <= arr.[mid] then ok <- mid else ng <- mid
        ok

    let upper (arr: int64 array) n a =
        let mutable (ok, ng) = (n, -1)
        while abs (ok - ng) > 1 do
            let mid = (ok + ng) / 2
            if a < arr.[mid] then ok <- mid else ng <- mid
        ok

    let mutable ans = 0L
    // a < b < c なので
    // 真ん中のBを固定させたら楽
    for i in 0 .. N - 1 do
        let b = B.[i]
        let oka = lower A N b
        let okc = upper C N b

        let elma = int64 oka
        let elmc = N - okc |> int64
        ans <- ans + (elma * elmc)

    puts ans
    ()

main()
writer.Close()
