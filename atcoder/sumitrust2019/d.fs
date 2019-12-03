open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let N = read int
    let S = read string |> Seq.toArray |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

    let mutable ans = 0
    for i in 0..999 do
        let c = [|i / 100; (i / 10) % 10; i % 10|]
        seq {
            let mutable cnt = 0
            for j in 0..N-1 do
                if S.[j] = c.[cnt] then
                    cnt <- cnt + 1

                if cnt = 3 then yield ()
        }
        |> fun s ->
            if Seq.isEmpty s |> not then
                ans <- ans + 1

    printfn "%d" ans
    0 // return an integer exit code
