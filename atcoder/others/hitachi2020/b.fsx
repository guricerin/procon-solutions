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

// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------

let main() =
    let [| A; B; M |] = reada int
    let ass = reada int64
    let bs = reada int64
    let xycs = Array.zeroCreate M
    for i in 0 .. M - 1 do
        let [| x; y; c |] = reada int64
        let x, y = int x - 1, int y - 1
        xycs.[i] <- x, y, c

    // 候補は2つ
    // 1. 割引券をつかわない場合の最安値
    // 2. 割引券をつかう
    // 割引券を使えるのは1回なので、O(n)で解ける
    let naxa = ass |> Array.min
    let naxb = bs |> Array.min
    let mutable ans = naxa + naxb
    for i in 0 .. M - 1 do
        let x, y, c = xycs.[i]
        let a, b = ass.[x], bs.[y]
        let tmp = a + b - c
        if ans > tmp then ans <- tmp

    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
