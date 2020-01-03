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
    let strRev (s: string): string =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

    let inline roundup (x: ^a) (y: ^a): ^a =
        let one = LanguagePrimitives.GenericOne
        (x + y - one) / y

[<EntryPoint>]
let main _ =
    let [|H;W;K;V|] = reada int64
    let H, W = int H, int W
    let A = Array.zeroCreate H
    for y in 0..H-1 do
        A.[y] <- reada int64

    let acc = Array2D.zeroCreate (H+1) (W+1)
    for y in 0..H-1 do
        for x in 0..W-1 do
            acc.[y+1, x+1] <- acc.[y, x+1] + acc.[y+1,x] - acc.[y,x] + A.[y].[x]

    let mutable maxrect = 0
    for y1 in 0..H do
        for y2 in y1+1..H do
            for x1 in 0..W do
                for x2 in x1+1..W do
                    let rect = (y2 - y1) * (x2 - x1)
                    let krect = K * int64 rect
                    let price = acc.[y2,x2] - acc.[y1, x2] - acc.[y2,x1] + acc.[y1,x1]
                    let total = price + krect
                    if total <= V then
                        // printfn "total: %d, rect: %d" total rect
                        maxrect <- max maxrect rect

    printfn "%d" maxrect
    0 // return an integer exit code
