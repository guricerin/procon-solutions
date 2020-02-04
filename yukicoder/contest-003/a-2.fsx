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

let bitcount (a: int) =
    a
    |> fun x -> Convert.ToString(a, 2)
    |> fun x -> x.Replace("0", "")
    |> fun x -> x.Length

let main() =
    // 想定解は幅優先探索
    let n = read int
    let inf = Int32.MaxValue
    let dist = Array.init (n + 1) (fun _ -> inf)
    let len = dist |> Array.length
    dist.[1] <- 1
    let que = Queue<int>()
    que.Enqueue(1)
    let inner a = 1 <= a && a < len
    while que.Count > 0 do
        let i = que.Dequeue()
        let b = bitcount i
        let l = i - b
        let r = i + b
        let next = dist.[i] + 1
        if inner l && dist.[l] > next then
            dist.[l] <- next
            que.Enqueue(l)
        if inner r && dist.[r] > next then
            dist.[r] <- next
            que.Enqueue(r)

    if dist.[n] = inf then -1 else dist.[n]
    |> puts
    ()

main()
writer.Close()
