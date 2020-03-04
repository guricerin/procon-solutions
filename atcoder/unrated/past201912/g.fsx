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
    let N = read int
    let ps = Array.zeroCreate N
    for i in 0 .. N - 2 do
        let a = reada int
        ps.[i] <- a

    let mutable nax = Int32.MinValue

    let rec dfs depth (ls: int array) =
        if depth = N then
            // printfn "depth:%d" depth
            // String.Join(" ", ls) |> printfn "%s"
            let mutable res = 0
            for i in 0 .. N - 1 do
                for j in i + 1 .. N - 1 do
                    if ls.[i] = ls.[j] then res <- res + ps.[i].[j - i - 1]
            nax <- max nax res
        else
            for i in 0 .. 2 do
                let l = ls |> Array.copy
                l.[depth] <- i
                dfs (depth + 1) l

    let ls = Array.zeroCreate N
    dfs 0 ls
    puts nax

    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
