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
    let [| A; B; K |] = reada int

    let ab = [| A; B |]
    for i in 0 .. K - 1 do
        let j = i % 2
        let k = (i + 1) % 2

        let c =
            if ab.[j] % 2 = 0 then ab.[j]
            else ab.[j] - 1

        let c = c / 2
        ab.[j] <- c
        ab.[k] <- ab.[k] + c

    printfn "%d %d" ab.[0] ab.[1]
    0 // return an integer exit code
