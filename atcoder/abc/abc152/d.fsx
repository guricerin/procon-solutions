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
    // 先頭がi, 末尾がjである数値の個数
    let nums = Array2D.init 10 10 (fun _ _ -> 0L)

    for i in 1 .. n do
        let s = string i
        let a = Seq.head s |> fun x -> int x - int '0'
        let b = Seq.last s |> fun x -> int x - int '0'
        nums.[a, b] <- nums.[a, b] + 1L
    let mutable ans = 0L
    for i in 1 .. 9 do
        for j in 1 .. 9 do
            let a = nums.[i, j] * nums.[j, i]
            ans <- ans + a
    puts ans
    ()

main()
writer.Close()
writer.Close()
