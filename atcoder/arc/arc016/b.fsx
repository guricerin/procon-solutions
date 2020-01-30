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
    let n = read int
    let maze = Array2D.init n 9 (fun _ _ -> '.')
    for i in 0 .. n - 1 do
        let x = read string
        for j in 0 .. 8 do
            maze.[i, j] <- x.[j]

    let mutable cnt = 0
    for x in 0 .. 8 do
        let mutable yu = 0
        while yu < n do
            let mutable yd = yu
            let c = maze.[yu, x]
            match c with
            | 'x' ->
                cnt <- cnt + 1
                yu <- yu + 1
            | 'o' ->
                cnt <- cnt + 1
                while yd < n && maze.[yd, x] = 'o' do
                    yd <- yd + 1
                if yu = yd then yu <- yu + 1 else yu <- yd
            | _ -> yu <- yu + 1

    puts cnt

main()
writer.Close()
