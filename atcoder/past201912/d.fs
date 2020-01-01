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

[<EntryPoint>]
let main _ =
    let n = read int
    let ass = Array.zeroCreate n
    for i in 0..n-1 do
        let a = read int
        let a = a - 1
        ass.[a] <- ass.[a] + 1

    if Array.forall (fun x -> x = 1) ass then
        printfn "Correct"
    else
        let mutable zero = 0
        let mutable mult = 0
        for i in 0..n-1 do
            if ass.[i] = 0 then zero <- i + 1
            if ass.[i] = 2 then mult <- i + 1
        printfn "%d %d" mult zero
    0 // return an integer exit code
