open System

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

[<EntryPoint>]
let main _ =
    let [|n;a;b|] = reada int
    let f n =
        let rec loop n acc =
            if n <= 0 then acc
            else loop (n / 10) (acc + n % 10)
        loop n 0

    let g x =
        let y = f x
        a <= y && y <= b

    [0..n]
    |> Seq.filter g
    |> Seq.sum
    |> printfn "%d"
    0
