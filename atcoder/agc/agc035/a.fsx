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

// XOR
// 交換則を満たす：a xor b = b xor a
// 結合則を満たす：(a xor b) xor c = a xor (b xor c)
// 分配則を満たす：x * (a xor b) = (x * a) xor (x * b)
// 単位元：0
// 移項ができる：
// a xor b = c
// a xor b xor b = c xor b  <- 両辺に b を xor 
// a xor 0 = c xor b
// a = c xor b 

let main() =
    let N = read int
    let A = reada int
    let memo = SortedDictionary<int, int>()
    for a in A do
        if memo.ContainsKey(a) then memo.[a] <- memo.[a] + 1 else memo.Add(a, 1)

    let mutable ok = false
    if memo.Count = 3 && N%3=0 then 
        let ok1 = 
            memo
            |> Seq.forall (fun x -> x.Value = N/3)

        let ok2 = 
            memo
            |> Seq.map (fun kv -> kv.Key)
            |> Seq.reduce (^^^)
            |> fun x -> x = 0
        ok <- ok1 && ok2
    else if memo.Count = 2 && N%3=0 then 
        let a = Array.ofSeq memo
        if a.[0].Key = 0 && a.[0].Value = N/3 && a.[1].Value = N/3*2 then ok <- true
    else if memo.Count = 1 && memo.ContainsKey(0) then ok <- true

    if ok then "Yes" else "No"
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
