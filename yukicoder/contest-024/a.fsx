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
    let mp = Array.init 10 (fun i -> (true, 0))
    for i in 0 .. n - 1 do
        let s = reada string
        let yn = s.[4]
        let xs = Array.map (fun x -> Convert.ToInt32(x.ToString())) s.[0..3]
        match yn with
        | "YES" ->
            xs
            |> Array.iter (fun x ->
                let (b, c) = mp.[x]
                mp.[x] <- (b, c + 1))
        | _ ->
            xs
            |> Array.iter (fun x ->
                let (b, c) = mp.[x]
                mp.[x] <- (false, c))

    mp
    |> Array.indexed
    |> Array.map (fun (i, (b, c)) ->
        if b then (i, c) else (i, -1))
    |> Array.maxBy (fun (i, c) -> c)
    |> fun (i, c) -> i
    |> puts

    ()

main()
writer.Close()
