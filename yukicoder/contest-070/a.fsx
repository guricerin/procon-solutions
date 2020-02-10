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

// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------

let main() =
    let n = read int
    let ws = reada int |> Array.sortDescending
    let m = read int
    let bs = reada int |> Array.sortDescending
    let wbs = Array.zeroCreate 2
    wbs.[0] <- ws
    wbs.[1] <- bs

    let rec meguru cur target acc =
        let ano = (cur + 1) % 2
        let mutable ok, ng = wbs.[ano] |> Array.length, -1
        while abs (ok - ng) > 1 do
            let mid = (ok + ng) / 2
            if wbs.[ano].[mid] < target then ok <- mid else ng <- mid

        if ok <> Array.length wbs.[ano] then meguru ano wbs.[ano].[ok] (acc + 1) else acc

    // 1段目が黒のパターンと白のパターンのうちでかいほう
    let nax = max (meguru 0 ws.[0] 1) (meguru 1 bs.[0] 1)
    puts nax
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
