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
    let [| a; b; c; x; y |] = reada int64
    let cand1 = a * x + b * y
    let cand2 = 2L * c * (max x y)

    let cand3 =
        let nin, nax = min x y, max x y
        let diff = nax - nin

        let m =
            if nax = x then a * diff else b * diff
        2L * nin * c + m

    List.min [ cand1; cand2; cand3 ] |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
