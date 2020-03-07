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
    let [| a; b |] = reada int
    seq {
        for i in 1 .. 10000 do
            let f = float i
            let eight = f * 0.08 |> int
            let ten = f * 0.1 |> int
            if eight = a && ten = b then yield i
    }
    |> Seq.tryHead
    |> function
    | Some(x) -> x
    | _ -> -1
    |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
