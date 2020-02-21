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
    let n = read int
    let ass = reada int
    // 連続する1の個数 + 1 マスジャンプできるサイコロがほしい
    let mutable l = 0
    let mutable ans = 0
    while l < n do
        let mutable r = l
        while r < n && ass.[r] = 1 do
            r <- r + 1
        let tmp = r - l
        ans <- max ans tmp
        if r = l then l <- r + 1 else l <- r

    puts (ans + 1)
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
