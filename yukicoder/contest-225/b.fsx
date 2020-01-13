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
    let N = read int
    let AS = Array.init N (fun _ -> Queue<int>())
    let mutable sum = 0
    for i in 0 .. N - 1 do
        let x = reada int
        let p = x.[0]
        sum <- sum + p
        for j in 1 .. p do
            AS.[i].Enqueue(x.[j])

    for _ in 0 .. sum - 1 do
        for A in AS do
            if A.Count > 0 then
                let a = A.Dequeue()
                sprintf "%d " a |> print
    puts ""

    ()

main()
writer.Close()
