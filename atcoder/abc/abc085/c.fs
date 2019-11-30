open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let [|N;Y|] = reada int

    seq {
        for i in 0..N do
            for j in 0..N-i do
                let k = N-i-j
                yield (i, j, k)
    }
    |> Seq.tryFind (fun (x, y, z) -> 10000 * x + 5000 * y + 1000 * z = Y)
    |> function
        | Some a -> a
        | None -> (-1, -1, -1)
    |> fun (x, y, z) -> printfn "%d %d %d" x y z
    0
