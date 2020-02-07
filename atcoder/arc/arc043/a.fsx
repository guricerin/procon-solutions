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
    // 式変形すると、
    // A * N = P * S_0 + Q + P * S_1 + Q ...
    // -> PS + NQ = AN
    // -> Q = A - (PS / N)
    // B = P * (T_max - T_min)
    // -> B = P * (S_max - S_min)
    // -> P = B / diff
    let [| n; a; b |] = reada int
    let a, b = float a, float b
    let ss = Array.zeroCreate n
    for i in 0 .. n - 1 do
        ss.[i] <- read int64
    let ss = ss |> Array.sort
    let diff = Array.last ss - Array.head ss
    if diff = 0L then
        printfn "-1"
        exit 0
    let p = b / float diff

    let sum =
        ss
        |> Array.sum
        |> float

    let q = a - (p * sum / float n)
    sprintf "%f %f" p q |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
