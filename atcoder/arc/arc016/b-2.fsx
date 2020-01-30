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
        for y in 0 .. n - 1 do
            let c = maze.[y, x]
            match c with
            | 'x' -> cnt <- cnt + 1
            | 'o' ->
                // ひとつ上がoならカウントしない、としたほうが楽
                if y > 0 && maze.[y - 1, x] = 'o' then () else cnt <- cnt + 1
            | _ -> ()

    puts cnt

main()
writer.Close()
