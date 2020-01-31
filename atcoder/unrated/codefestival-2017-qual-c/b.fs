open System

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
    let N = read int
    let A = reada int

    let total = 3.0 ** (float N) |> int
    seq {
        for i in 0 .. N - 1 do
            if A.[i] % 2 = 0 then yield 2
            else yield 1
    }
    |> Seq.fold (fun acc e -> acc * e) 1
    |> fun x -> total - x
    |> printfn "%d"
    0 // return an integer exit code
