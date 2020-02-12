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
    let bs = reada int64
    let fs = Array.zeroCreate (n + 1)
    let gs = Array.zeroCreate (n + 1)
    for i in 0 .. n - 2 do
        if bs.[i] <= bs.[i + 1] then fs.[i] <- 1L
        if bs.[i] >= bs.[i + 1] then gs.[i] <- 1L
    let fcum = Array.zeroCreate (n + 1)
    let gcum = Array.zeroCreate (n + 1)
    for i in 0 .. n - 1 do
        fcum.[i + 1] <- fcum.[i] + fs.[i]
        gcum.[i + 1] <- gcum.[i] + gs.[i]
    let q = read int
    for i in 0 .. q - 1 do
        let [| l; r |] = reada int
        let diff = r - l |> int64

        let okf =
            if fcum.[r] - fcum.[l] = diff then 1 else 0

        let okg =
            if gcum.[r] - gcum.[l] = diff then 1 else 0

        sprintf "%d %d" okf okg |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
