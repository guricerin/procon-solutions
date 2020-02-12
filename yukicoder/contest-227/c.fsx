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
    let bs = Array.append bs [| (Array.last bs) |]
    let fs = Array.zeroCreate (n + 1)
    let gs = Array.zeroCreate (n + 1)
    for i in 0 .. n - 1 do
        fs.[i + 1] <- fs.[i] + if bs.[i] <= bs.[i + 1] then 1 else 0
        gs.[i + 1] <- gs.[i] + if bs.[i] >= bs.[i + 1] then 1 else 0
    let q = read int
    for i in 0 .. q - 1 do
        let [| l; r |] = reada int
        let diff = r - l

        let okf =
            if fs.[r] - fs.[l] = diff then 1 else 0

        let okg =
            if gs.[r] - gs.[l] = diff then 1 else 0

        sprintf "%d %d" okf okg |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
