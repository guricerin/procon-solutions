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
    let dp = Array.zeroCreate (n + 10)
    dp.[0] <- 0L
    dp.[1] <- 1L
    dp.[2] <- 2L
    for i in 3 .. n do
        let a = dp.[i - 2]
        let b = dp.[i - 1]
        dp.[i] <- a + b

    dp.[n] |> puts
    ()

main()
writer.Close()
