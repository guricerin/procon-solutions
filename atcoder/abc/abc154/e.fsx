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
    // https://www.hamayanhamayan.com/entry/2020/02/09/225141
    let N = readInts()
    let K = read int
    // 左からx桁目まで確定、N未満である、0がちょうどK個ある個数
    let dp = Array3D.init 110 2 5 (fun _ _ _ -> 0L)
    dp.[0, 0, 0] <- 1L
    let len = N |> Array.length
    for dgt in 0 .. len - 1 do
        for isless in 0 .. 1 do
            for k in 0 .. K do
                let n = N.[dgt] // 左からdgt桁目の数値
                for next in 0 .. 9 do
                    if n < next && isless = 0 then
                        ()
                    else
                        let dgt2 = dgt + 1

                        let isless2 =
                            if next < n then 1 else isless

                        let k2 =
                            if next <> 0 then k + 1 else k

                        dp.[dgt + 1, isless2, k2] <- dp.[dgt + 1, isless2, k2] + dp.[dgt, isless, k]

    // 答えはN以下の整数なので、N自身も含める
    let ans = dp.[len, 0, K] + dp.[len, 1, K]
    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
