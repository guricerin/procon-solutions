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
    let w = read int
    let h = read int
    let maze = Array2D.init h w (fun _ _ -> 0)
    for i in 0 .. h - 1 do
        let l = reada int
        for j in 0 .. w - 1 do
            maze.[i, j] <- l.[j]

    let dxy =
        [ (-1, 0)
          (0, -1)
          (0, 1)
          (1, 0) ]

    let inner y x = 0 <= y && y < h && 0 <= x && x < w

    let mutable visit = Array2D.init h w (fun _ _ -> false)

    let rec dfs sy sx acc =
        visit.[sy, sx] <- true
        let d = acc + 1
        let mutable acc = acc + 1
        for (y, x) in dxy do
            let ny, nx = sy + y, sx + x
            if inner ny nx && maze.[ny, nx] = 1 && not visit.[ny, nx] then acc <- max acc (dfs ny nx d)
        visit.[sy, sx] <- false
        acc

    let mutable ans = 0

    for y in 0 .. h - 1 do
        for x in 0 .. w - 1 do
            if maze.[y, x] = 1 then
                visit <- Array2D.init h w (fun _ _ -> false)
                ans <- max ans (dfs y x 0)

    puts ans

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
