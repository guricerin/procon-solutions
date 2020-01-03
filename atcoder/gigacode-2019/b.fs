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
    let [|N;X;Y;Z|] = reada int
    let ab = Array.zeroCreate N
    for i in 0..N-1 do
        ab.[i] <- reada int

    ab
    |> Array.filter (fun arr ->
        let a = arr.[0]
        let b = arr.[1]
        a >= X && b >= Y && a + b >= Z)
    |> Array.length
    |> printfn "%d"
    0 // return an integer exit code
