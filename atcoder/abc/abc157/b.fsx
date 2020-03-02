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
    let maze = Array2D.init 3 3 (fun _ _ -> 0)
    for i in 0 .. 2 do
        let a = reada int
        for j in 0 .. 2 do
            maze.[i, j] <- a.[j]

    let N = read int
    let B = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let b = read int
        B.[i] <- b

    let seen = Array2D.init 3 3 (fun _ _ -> false)
    for b in B do
        for i in 0 .. 2 do
            for j in 0 .. 2 do
                if maze.[i, j] = b then seen.[i, j] <- true

    let mutable ok = false
    for i in 0 .. 2 do
        if seen.[i, *] |> Array.forall id then ok <- true
        if seen.[*, i] |> Array.forall id then ok <- true
    if seen.[0, 0] && seen.[1, 1] && seen.[2, 2] then ok <- true
    if seen.[0, 2] && seen.[1, 1] && seen.[2, 0] then ok <- true

    if ok then "Yes" else "No"
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
