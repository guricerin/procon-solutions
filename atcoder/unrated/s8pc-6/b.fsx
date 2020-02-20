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
    let abs = Array.zeroCreate n
    for i in 0 .. n - 1 do
        let [| a; b |] = reada int64
        abs.[i] <- (a, b)

    seq {
        for l in 0 .. n - 1 do
            for r in 0 .. n - 1 do
                let l, r = fst abs.[l], snd abs.[r]
                let mutable total = 0L
                for i in 0 .. n - 1 do
                    let a, b = abs.[i]

                    let cost =
                        match l <= a, b <= r with
                        | true, true -> r - l // 完全被覆
                        | true, _ -> (b - l) + (b - r) // aが入口～出口間からはみでている
                        | _, true -> (l - a) + (r - a) // bが入口～出口間からはみでている
                        | _ -> (l - a) + (b - a) + (b - r) // a,bともに入口～出口間からはみでている
                    total <- total + cost
                yield total
    }
    |> Seq.min
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
