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
    let ys = reada int64
    let ysum = ys |> Array.sum
    let xs = Array.zeroCreate n
    // 式変形すると
    // xi = -(n-2) * yi + (yの総和からyiを引いた値)
    for i in 0..n-1 do
        let a = (n-2) * -1 |> int64
        let x = (a * ys.[i]) + (ysum - ys.[i])
        xs.[i] <- x

    let ans = String.Join(" ", xs)
    ans |> puts
    ()

main()
writer.Close()
