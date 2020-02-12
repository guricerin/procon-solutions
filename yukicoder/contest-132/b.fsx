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
    let s = readChars()
    let len = s |> Array.length

    // 右にあるwの個数
    let cum =
        Array.scan (fun acc c ->
            if c = 'w' then 1L else 0L) 0L s
    for i in len .. -1 .. 1 do
        cum.[i - 1] <- cum.[i - 1] + cum.[i]

    let cpos = ResizeArray<int>()
    for i, c in Array.indexed s do
        if c = 'c' then cpos.Add(i)

    // 右にあるwの個数から2通り選ぶ組み合わせの総和
    let mutable ans = 0L
    for p in cpos do
        let w = cum.[p + 1]
        let t = w * (w - 1L) / 2L // w_C_2
        ans <- ans + t
    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
