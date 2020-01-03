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

type Pair = int * int

[<EntryPoint>]
let main _ =
    let N = read int
    let A = Array.zeroCreate N
    let xys = Array.zeroCreate N
    for i in 0..N-1 do
        let a = read int
        A.[i] <- a
        let xy = ResizeArray<Pair>()
        for _ in 0..a-1 do
            let [|x;y|] = reada int
            xy.Add((x-1,y))
        xys.[i] <- xy

    let mutable ans = 0
    let lim = (1 <<< N)
    for bit in 0..lim-1 do
        let mutable ok = true
        let mutable cnt = 0
        // i番目の人物を正直者と仮定
        for i in 0..N-1 do
            let bi = bit &&& (1 <<< i)
            if bi > 0 then
                cnt <- cnt + 1
                for xy in xys.[i] do
                    let x,y = xy
                    let bx = bit &&& (1 <<< x)
                    let bx = if bx > 0 then 1 else 0
                    // 仮定と実際の証言が矛盾しているか検証
                    if y <> bx then ok <- false

        if ok then
            // printfn "bit: %d, ans: %d, cnt: %d, ok: %b" bit ans cnt ok
            ans <- max ans cnt

    printfn "%d" ans

    0 // return an integer exit code
