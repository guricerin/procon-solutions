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
    let [| n; m |] = reada int
    let bs = reada string
    let op = bs.[0]

    let op =
        if op = "+" then (+) else (*)

    let bs = bs.[1..] |> Array.map (fun x -> Convert.ToInt64(x))
    let ass = Array.zeroCreate n
    for i in 0 .. n - 1 do
        let a = read int64
        let ans = Array.map (fun b -> op a b) bs
        String.Join(" ", ans) |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
