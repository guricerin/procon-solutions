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
    let [| H; W |] = reada int
    let maze = Array2D.init H W (fun _ _ -> 'k')
    for i in 0 .. H - 1 do
        maze.[i, *] <- readChars()

    let dp = Array2D.init (H + 10) (W + 10) (fun _ _ -> Int32.MaxValue)
    dp.[0, 0] <- 0
    for y in 0 .. H - 1 do
        for x in 0 .. W - 1 do
            let d =
                if maze.[y, x] = 'k' then y + x else 0
            if y > 0 then dp.[y, x] <- min dp.[y, x] (dp.[y - 1, x] + 1 + d)
            if x > 0 then dp.[y, x] <- min dp.[y, x] (dp.[y, x - 1] + 1 + d)

    dp.[H - 1, W - 1] |> puts
    ()

main()
writer.Close()
