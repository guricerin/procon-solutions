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
    read int |> ignore
    let a = Array.zeroCreate 3
    for i in 0 .. 2 do
        let s = read string
        if s.Contains(" ") then
            printfn "\"assert\""
            exit 0
        a.[i] <- Convert.ToInt64(s)

    let lim = a |> Array.length
    let b = ResizeArray<int64>()
    for i in 0 .. lim - 1 do
        for j in i + 1 .. lim - 1 do
            let c = a.[i] + a.[j]
            b.Add(c)

    let b = b.ToArray()

    let b =
        b
        |> Array.sort
        |> Array.rev
        |> Array.distinct

    b.[1] |> puts

    ()

main()
writer.Close()
