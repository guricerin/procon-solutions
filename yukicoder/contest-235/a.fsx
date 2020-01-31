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
    let [| x; n; m |] = reada int
    let A = reada int
    let B = reada int
    let oka = A |> Array.contains x
    let okb = B |> Array.contains x
    match oka, okb with
    | true, true -> "MrMaxValu"
    | true, _ -> "MrMax"
    | _, true -> "MaxValu"
    | _ -> "-1"
    |> puts
    ()

main()
writer.Close()
