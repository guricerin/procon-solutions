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
    let n = read int
    let len = 10100
    let lim = Int32.MaxValue
    let dp = Array.init len (fun _ -> lim)
    dp.[0] <- 0
    dp.[1] <- 1
    for _ in 0 .. 10 do
        for i in 1 .. 10000 do
            let b = bitcount i
            let l = i - b
            let r = i + b
            let ok = dp.[i] <> lim
            if 1 <= l && l <= n && ok then dp.[l] <- min dp.[l] (dp.[i] + 1)
            if 1 <= r && r <= n && ok then dp.[r] <- min dp.[r] (dp.[i] + 1)

    if dp.[n] = lim then -1 else dp.[n]
    |> puts
    ()

main()
writer.Close()
