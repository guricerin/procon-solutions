open System

[<AutoOpen>]
module Cin =
    let readInt() = stdin.ReadLine() |> int
    let readLong() = stdin.ReadLine() |> int64
    let readLongArr() = stdin.ReadLine().Split(' ') |> Array.map int64
    let readString() = stdin.ReadLine()

let n = readInt()
let mutable ans = "No"

for a in 1 .. 9 do
    for b in 1 .. 9 do
        if a * b = n then ans <- "Yes"

printfn "%s" ans
