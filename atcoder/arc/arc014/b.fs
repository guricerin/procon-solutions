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
    let W = Array.zeroCreate N
    for i in 0..N-1 do
        W.[i] <- read string

    let mutable last = W.[0] |> Seq.head
    let mutable mp = Map.ofList []
    seq {
        for i in 0..N-1 do
            let w = W.[i]
            if Seq.head w <> last then
                yield i
            else
                last <- w |> Seq.last
                match Map.tryFind w mp with
                | Some v ->
                    yield i
                | None ->
                    mp <- Map.add w 1 mp
    }
    |> fun s ->
        let s = Array.ofSeq s
        if Array.isEmpty s then "DRAW"
        else
            let i = Array.head s
            if i % 2 = 0 then "LOSE"
            else "WIN"
    |> printfn "%s"

    0 // return an integer exit code
