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
    let [| n; l |] = reada int
    let width = 2 * n - 1
    let maze = Array2D.init (l + 1) width (fun _ _ -> '.')
    for i in 0 .. l do
        let s = readChars() |> Array.indexed
        for (j, c) in s do
            maze.[i, j] <- c

    let start =
        seq {
            for i in 0 .. width - 1 do
                if maze.[l, i] = 'o' then yield i
        }
        |> Seq.head

    let mutable ans = 0
    let inner y x = (0 <= y && y < l) && (0 <= x && x < width)

    let rec dfs y x =
        if y < 0 then
            ans <- x
        else
            let l, r = x - 1, x + 1
            if inner y l && maze.[y, l] = '-' then dfs (y - 1) (l - 1)
            else if inner y r && maze.[y, r] = '-' then dfs (y - 1) (r + 1)
            else dfs (y - 1) x

    dfs (l - 1) start

    (ans / 2) + 1 |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
