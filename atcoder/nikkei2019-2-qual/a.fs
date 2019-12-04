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
    let n = read int
    let ans =
        if n % 2 = 0 then n / 2 - 1
        else n / 2
    let ans = if n < 3 then 0 else ans

    ans |> printfn "%d"
    0 // return an integer exit code
