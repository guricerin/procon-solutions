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

let main() =
    let [| H; N |] = reada int
    let abs = Array.zeroCreate N
    for i in 0 .. N - 1 do
        let [| a; b |] = reada int
        abs.[i] <- (a, b)

    // dp[i][h] : i番目までの魔法からいくつかの魔法を選んでダメージの総和がhとなる方法のうち、消費魔力が最小となる値
    let inf = 1e9 |> int
    let dp = Array2D.init (N + 10) (H + 10) (fun _ _ -> inf)
    dp.[0, 0] <- 0
    for i in 0 .. N - 1 do
        let a, b = abs.[i]
        for h in 0 .. H do
            // i番目の魔法を使わない場合
            dp.[i + 1, h] <- min dp.[i + 1, h] dp.[i, h]
            // i番目の魔法を使う場合
            let lim = min (a + h) H // Hを超える分は無駄なのでスキップ
            dp.[i + 1, lim] <- min dp.[i + 1, lim] (dp.[i + 1, h] + b)

    let ans = dp.[*, H] |> Array.min
    puts ans

main()
writer.Close()
