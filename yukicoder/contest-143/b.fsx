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
    let maze = Array2D.init n n (fun _ _ -> 0)

    let mutable d = 0

    let nextD() = d <- (d + 1) % 4

    let mutable (x, y) = (0, 0)

    let step x y =
        match d with
        | 0 -> x + 1, y
        | 1 -> x, y + 1
        | 2 -> x - 1, y
        | _ -> x, y - 1

    let isOut x y = not (0 <= x && x < n && 0 <= y && y < n)
    for i in 0 .. n * n - 1 do

        let a = i + 1
        maze.[y, x] <- a
        let mutable (nx, ny) = step x y
        if isOut nx ny then
            nextD()
            let a, b = step x y
            nx <- a
            ny <- b
        else if maze.[ny, nx] <> 0 then
            nextD()
            let a, b = step x y
            nx <- a
            ny <- b

        x <- nx
        y <- ny

    for i in 0 .. n - 1 do
        let s = maze.[i, *] |> Array.map (fun x -> sprintf "%03d" x)
        String.Join(" ", s) |> puts

main()
writer.Close()
