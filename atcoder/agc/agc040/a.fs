open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readChars() = read string |> Seq.toArray
    let readInts() =
        readChars()
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

[<AutoOpen>]
module Cout =
    let writer = new IO.StreamWriter(new IO.BufferedStream(Console.OpenStandardOutput()))
    let print (s: string) = writer.Write s
    let println (s: string) = writer.WriteLine s
    let inline puts (s: ^a) = s |> string |> println

[<AutoOpen>]
module Util =
    let strRev (s: string): string =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

    let inline roundup (x: ^a) (y: ^a): ^a =
        let one = LanguagePrimitives.GenericOne
        (x + y - one) / y

    let inline sameParity (x: ^a) (y: ^a): bool =
        let one = LanguagePrimitives.GenericOne
        (x &&& one) = (y &&& one)

let f n = n * (n+1) / 2

let solve() =
    let s = readChars()

    let len = Array.length s
    let left = Array.zeroCreate (len+1)
    for i in 0..len-1 do
        if s.[i] = '<' then
            left.[i+1] <- left.[i] + 1L
        else
            left.[i+1] <- 0L
    let right = Array.zeroCreate (len+1)
    for i in len-1..-1..0 do
        if s.[i] = '>' then
            right.[i] <- right.[i+1] + 1L
        else
            right.[i] <- 0L

    seq {
        for i in 0..len do
            yield max left.[i] right.[i]
    }
    |> Seq.sum
    |> puts
    ()

[<EntryPoint>]
let main _ =
    solve()
    writer.Close()
    0 // return an integer exit code
