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
    let A = reada int
    // 操作回数はnを超えない
    let mutable ans = n
    // 右から見てでかい数が降順に整列している個数を数える
    // 左から見て小さい数が整列しているかどうかはどうでもいい
    let mutable now, cnt = n, 0
    for i in n - 1 .. -1 .. 0 do
        let a = A.[i]
        if a = now then
            cnt <- cnt + 1
            now <- now - 1

    let ans = max 0 (ans - cnt)
    ans |> puts
    ()

main()
writer.Close()
