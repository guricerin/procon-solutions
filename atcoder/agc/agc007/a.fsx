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

let [| H; W |] = reada int
let maze = Array2D.init H W (fun _ _ -> '.')

for y in 0 .. H - 1 do
    let s = read string
    for x in 0 .. W - 1 do
        maze.[y, x] <- s.[x]

let mutable ok = true

for y in 0 .. H - 2 do
    let mutable r = 0
    for x in 0 .. W - 1 do
        if maze.[y, x] = '#' then r <- x
    for x in r - 1 .. -1 .. 0 do
        if maze.[y + 1, x] = '#' then ok <- false

if ok then "Possible" else "Impossible"
|> puts

writer.Dispose()
