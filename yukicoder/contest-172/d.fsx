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
    let [| n; d |] = reada int
    let d = int64 d
    let ts = Array.zeroCreate n
    let ks = Array.zeroCreate n
    for i in 0 .. n - 1 do
        let [| t; k |] = reada int64
        ts.[i] <- t
        ks.[i] <- k

    let inf = Int64.MinValue
    let dp = Array2D.init (n + 10) 2 (fun _ _ -> inf)
    dp.[0, 0] <- 0L
    dp.[0, 1] <- -d // こちらの初期化に注意
    for i in 1 .. n do
        let t, k = ts.[i - 1], ks.[i - 1]
        // 今日東京
        let a = dp.[i - 1, 0] + t // 昨日東京
        let b = dp.[i - 1, 1] + t - d // 昨日京都
        dp.[i, 0] <- max a b
        // 今日京都
        let a = dp.[i - 1, 1] + k // 昨日京都
        let b = dp.[i - 1, 0] + k - d // 昨日東京
        dp.[i, 1] <- max a b

    let ans = max dp.[n, 0] dp.[n, 1]
    puts ans
    ()

main()
writer.Close()
