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
    let bs = reada int
    let mp = Dictionary<int, int>()
    for b in bs do
        if mp.ContainsKey(b) then mp.[b] <- mp.[b] + 1 else mp.Add(b, 1)

    // 実験すると、奇数枚ある番号のカードは、共食いさせていくことで必ず1枚にできる
    // 偶数枚のカードは共食いさせたら1枚も残らない。が、偶数枚のカードが他にあれば、そいつと共に生贄に捧げていくことで、最終的に両方とも1枚にできる。
    // ということで、偶数枚のカードが偶数種類あれば、どうあがいても1種類は消滅する
    // ただ、公式の解説が頭良すぎ
    let mutable even, odd = 0, 0
    mp
    |> Seq.iter (fun kv ->
        let v = kv.Value
        if v % 2 = 0 then even <- even + 1 else odd <- odd + 1)
    let ans =
        if even % 2 = 0 then even + odd else (even - 1) + odd
    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
