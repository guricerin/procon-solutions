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

let solve() =
    let [| x; y; z |] = reada int64
    if y <= z then z - 2L
    elif x < z && z < y then z - 1L
    else 
        if z <= 1L then 0L
        else z
    |> puts
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> failwithf "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
