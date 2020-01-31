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
    let [| n; x |] = reada int
    let x = int64 x
    let ab = Array.zeroCreate n
    for i in 0 .. n - 1 do
        let [| a; b |] = reada int64
        ab.[i] <- (a, b)

    let mutable ans = ab |> Array.sumBy (fun (a, b) -> a * b)
    let nax = ab |> Array.maxBy (fun (a, b) -> b)
    let _, nax = nax
    ans <- ans + (nax * x)
    puts ans
    ()

main()
writer.Close()
