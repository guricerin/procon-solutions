open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

module Util =
    let strRev (s: string): string =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

    let inline roundup (x: ^a) (y: ^a): ^a =
        let one = LanguagePrimitives.GenericOne
        (x + y - one) / y

[<EntryPoint>]
let main _ =
    let D = read int
    let A = reada int64
    let B = reada int64

    let acc =
        A
        |> Array.scan (fun acc a -> acc + a) 0L

    seq {
        for i in 0..D-1 do
            if acc.[i+1] >= B.[i] then yield B.[i]
    }
    |> fun s -> if Seq.isEmpty s then -1L else s |> Seq.min
    |> printfn "%d"
    0 // return an integer exit code
