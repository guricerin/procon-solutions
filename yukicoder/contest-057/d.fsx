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
    let ls = reada int
    let nin = Array.min ls
    let ls = Array.map (fun x -> x - nin) ls |> List.ofArray

    let rec loop ls acc =
        let [ x; y; z ] = List.sort ls
        match x, y, z with
        | 0, 0, z when z < 5 -> acc
        | 0, 0, z when z >= 5 ->
            loop
                [ x
                  y
                  z - 5 ] (acc + 1)
        | 0, 1, z when z <= 2 -> acc
        | 0, 2, z when z <= 2 -> acc
        | 0, _, z when z > 2 ->
            loop
                [ x
                  y - 1
                  z - 3 ] (acc + 1)
        | _ ->
            loop
                [ x - 1
                  y - 1
                  z - 1 ] (acc + 1)

    let ans = nin + (loop ls 0)
    ans |> puts
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
