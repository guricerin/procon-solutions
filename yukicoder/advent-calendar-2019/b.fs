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
    let strRev s =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

    let inline roundup (a: ^t) (b: ^t): ^t =
        let one = LanguagePrimitives.GenericOne
        (a + b - one) / b

[<EntryPoint>]
let main _ =
    let [|a;b|] = reada int64

    if (a &&& b <> a) then
        printfn "0"
    else
        let mutable ca = 0
        let mutable cb = 0
        for i in 0..33 do
            if (a >>> i) &&& 1L = 1L then ca <- ca + 1
            if (b >>> i) &&& 1L = 1L then cb <- cb + 1
        let e = max (cb - ca - 1) 0
        let ans = 1 <<< e
        printfn "%d" ans
    0 // return an integer exit code
