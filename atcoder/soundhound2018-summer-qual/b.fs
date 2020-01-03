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
    let s = read string |> Array.ofSeq
    let w = read int

    seq {
        for i in 0..w..Array.length s - 1 do
            yield s.[i]
    }
    |> String.Concat
    |> printfn "%s"
    0 // return an integer exit code
