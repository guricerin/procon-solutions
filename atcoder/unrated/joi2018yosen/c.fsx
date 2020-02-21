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
    let [| h; w |] = reada int
    let maze = Array2D.init h w (fun _ _ -> 0)
    for y in 0 .. h - 1 do
        let a = reada int
        for x in 0 .. w - 1 do
            maze.[y, x] <- a.[x]

    // 25^4 ≒ 390000
    // 全探索でおｋ
    let mutable ans = Int32.MaxValue
    for y in 0 .. h - 1 do
        for x in 0 .. w - 1 do
            let mutable cost = 0
            for i in 0 .. h - 1 do
                for j in 0 .. w - 1 do
                    let d1 = abs (y - i)
                    let d2 = abs (x - j)
                    let d = (min d1 d2) * maze.[i, j]
                    cost <- cost + d
            ans <- min ans cost

    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
