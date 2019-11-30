open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let N = read int
    let A = reada int

    let ans = Array.zeroCreate N
    for i in 0..N-1 do
        ans.[A.[i] - 1] <- i + 1

    ans
    |> Array.iter (fun i -> printf "%d " i)
    printfn ""
    0
