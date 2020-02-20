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
    // 被覆数の多い区間が答え -> imos法
    let cums = Array.zeroCreate (int 1e6 + 10)
    for i in 0 .. n - 1 do
        let [| a; b |] = reada int
        cums.[a] <- cums.[a] + 1
        cums.[b + 1] <- cums.[b + 1] - 1

    for i in 0 .. (int 1e6) do
        cums.[i + 1] <- cums.[i + 1] + cums.[i]

    cums
    |> Array.max
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
