open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

module Util =
    let strRev s =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

let f p c =
    if p = c then "stay"
    else if p < c then sprintf "up %d" (c - p)
    else sprintf "down %d" (p - c)

[<EntryPoint>]
let main _ =
    let n = read int
    let ass = Array.zeroCreate n
    for i in 0..n-1 do
        ass.[i] <- read int

    for i in 1..n-1 do
        printfn "%s" (f ass.[i-1] ass.[i])
    0 // return an integer exit code
