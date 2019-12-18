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
    let A = Array.zeroCreate 5
    for i in 0 .. 4 do
        A.[i] <- read int64

    let mutable fib = new SortedSet<int64>()
    let mutable a = 0L
    let mutable b = 1L
    while a <= int64 1e15 do
        fib.Add(a) |> ignore
        fib.Add(b) |> ignore
        a <- a + b
        b <- b + a

    let f n =
        printfn "%d" n
        exit (0)

    let mutable ans = 0
    if fib.Contains(A.[4]) then
        ans <- 1
        for i in 4 .. -1 .. 1 do
            let a = A.[i]
            let b = A.[i - 1]
            if a <= b && fib.Contains(a) && fib.Contains(b) then ans <- ans + 1
            else f ans
    else
        f 0

    printfn "%d" ans
    0 // return an integer exit code
