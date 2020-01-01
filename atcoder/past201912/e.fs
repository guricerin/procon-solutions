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

let f (ss:int array) (graph: bool [,]) (len:int)=
    let a = ss.[1] - 1
    match ss.[0] with
    | n when n = 1 ->
        let b = ss.[2] - 1
        graph.[a, b] <- true
    | n when n = 2 ->
        for i in 0..len-1 do
            if graph.[i,a] then
                graph.[a, i] <- true
    | n when n = 3 ->
        let mutable st = set []
        for i in 0..len-1 do
            if graph.[a, i] then
                for j in 0..len-1 do
                    if graph.[i, j] then
                        st <- Set.add j st
        for y in st do
            graph.[a, y] <- true
        graph.[a, a] <- false
    | _ -> failwithf "hoge"

[<EntryPoint>]
let main _ =
    let [|n;q|] = reada int
    let ss = Array.zeroCreate q
    for i in 0..q-1 do
        ss.[i] <- reada int

    let graph = Array2D.init n n (fun _ _ -> false)
    for i in 0..q-1 do
        f ss.[i] graph n

    for i in 0..n-1 do
        let ans = graph.[i, 0..] |> Array.map (fun x -> if x then "Y" else "N") |> String.Concat
        printfn "%s" ans
    0 // return an integer exit code
