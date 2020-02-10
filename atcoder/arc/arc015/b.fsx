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
    let mts = Array.zeroCreate 8
    for i in 0 .. n - 1 do
        let [| m; t |] = reada float

        let i =
            match m with
            | _ when 35.0 <= m -> 0
            | _ when 30.0 <= m -> 1
            | _ when 25.0 <= m -> 2
            | _ -> 6

        let j =
            match t with
            | _ when 25.0 <= t -> 3
            | _ when t < 0.0 && 0.0 <= m -> 4
            | _ when m < 0.0 -> 5
            | _ -> 6

        mts.[i] <- mts.[i] + 1
        mts.[j] <- mts.[j] + 1

    String.Join(" ", mts.[..5]) |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
