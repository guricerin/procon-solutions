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
    let [|x;y|] = reada int

    let ans =
        let a =
            if x < 4 then 400000 - (x * 100000)
            else 0
        let b =
            if y < 4 then 400000 - (y * 100000)
            else 0
        let c = if x = 1 && y = 1 then 400000 else 0
        a + b + c

    printfn "%d" ans
    0 // return an integer exit code
