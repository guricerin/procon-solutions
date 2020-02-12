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
    let card = Array2D.init 4 13 (fun _ _ -> false)
    let mns = reada string
    for i in 0 .. n - 1 do
        let mn = mns.[i]

        let suit =
            match mn.[0] with
            | 'D' -> 0
            | 'C' -> 1
            | 'H' -> 2
            | 'S' -> 3
        let num =
            match mn.[1] with
            | 'A' -> 1
            | 'T' -> 10
            | 'J' -> 11
            | 'Q' -> 12
            | 'K' -> 13
            | c -> int c - int '0'

        let num = num - 1
        card.[suit, num] <- true

    for s in 0 .. 3 do
        for k in 0 .. 12 do
            let suit =
                match s with
                | 0 -> "D"
                | 1 -> "C"
                | 2 -> "H"
                | 3 -> "S"

            let num =
                match k with
                | 0 -> "A"
                | 9 -> "T"
                | 10 -> "J"
                | 11 -> "Q"
                | 12 -> "K"
                | _ -> string (k + 1)

            if card.[s, k] then sprintf "%s%s " suit num |> print
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
