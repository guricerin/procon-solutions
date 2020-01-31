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
    let n = read int
    let A = reada int64
    let inf = Int64.MaxValue
    // LIS(最長増加部分列)という典型
    // 参考：https://qiita.com/python_walker/items/d1e2be789f6e7a0851e5
    let dp = Array.init n (fun _ -> inf)
    for i in 0 .. n - 1 do
        let a = A.[i]
        let mutable (ok, ng) = (n, -1)
        while abs (ok - ng) > 1 do
            let mid = (ok + ng) / 2
            if a <= dp.[mid] then ok <- mid else ng <- mid
        if ok < n then dp.[ok] <- a

    dp
    |> Array.takeWhile (fun a -> a <> inf)
    |> Array.length
    |> puts
    ()

main()
writer.Close()
