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
    let vs = reada int

    let dp = Array2D.init (n + 10) 2 (fun _ _ -> 0)
    dp.[0, 0] <- 0
    dp.[0, 1] <- vs.[0]
    if n > 1 then
        dp.[1, 0] <- vs.[0]
        dp.[1, 1] <- vs.[1]
    for i in 2 .. n - 1 do
        let v = vs.[i]
        dp.[i, 0] <- Array.max dp.[i - 1, *]
        dp.[i, 1] <- Array.max dp.[i - 2, *] + v
    ()

    let ans = Array.max dp.[n - 1, *]
    puts ans

main()
writer.Close()
