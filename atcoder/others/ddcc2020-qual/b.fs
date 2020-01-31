open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

[<EntryPoint>]
let main _ =
    let N = read int
    let A = reada int64

    let sum = A |> Array.sum
    let mutable ans = Int64.MaxValue
    let mutable left = 0L
    for i in 0..N-1 do
        left <- left + A.[i]
        let right = sum - left
        ans <- Math.Min(ans, left - right |> abs)

    ans |> printfn "%d"
    0 // return an integer exit code
