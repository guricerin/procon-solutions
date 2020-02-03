open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f
    let readChars() = read string |> Seq.toArray
    let readInts() = readChars() |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

[<AutoOpen>]
module Cout =
    let writer = new IO.StreamWriter(new IO.BufferedStream(Console.OpenStandardOutput()))
    let print (s: string) = writer.Write s
    let println (s: string) = writer.WriteLine s
    let inline puts (s: ^a) = string s |> println

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

let main() =
    let [| p; q; r |] = reada int64
    let [| a; b; c |] = reada int64

    // 天井関数
    // ceil x/y = z のとき、以下が成り立つ
    // z - 1 < x/y <= z
    // (z - 1) * y < x <= z * y

    // 以下については、数直線を書いてみれば想像がつく
    let lower =
        List.max
            [ (a - 1L) * p + 1L
              (a + b - 1L) * q + 1L
              (a + b + c - 1L) * r + 1L ]

    let upper =
        List.min
            [ a * p
              (a + b) * q
              (a + b + c) * r ]

    if lower > upper then "-1" else sprintf "%d %d" lower upper
    |> puts
    ()

main()
writer.Close()
