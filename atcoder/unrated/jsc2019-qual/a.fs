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
    let [|M;D|] = reada int

    let mutable ans = 0
    for m in 1..M do
        for d in 10..D do
            let d1,d2 = d / 10, d % 10
            if d1 * d2 = m && d1 >= 2 && d2 >= 2 then
                ans <- ans + 1

    ans |> printfn "%d"
    0 // return an integer exit code
