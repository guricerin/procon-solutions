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
    let [| n; m |] = reada int
    let ps = Array2D.init n m (fun _ _ -> 0L)
    for i in 0 .. n - 1 do
        let a = reada int64
        for j in 0 .. m - 1 do
            ps.[i, j] <- a.[j]

    let mutable ans = 0L
    for i in 0 .. m - 1 do
        for j in 0 .. m - 1 do
            if i <> j then
                let mutable tmp = 0L
                for k in 0 .. n - 1 do
                    let a = max ps.[k, i] ps.[k, j]
                    tmp <- tmp + a
                ans <- max ans tmp

    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
