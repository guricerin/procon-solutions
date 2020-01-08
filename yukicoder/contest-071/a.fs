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

let ok i j k = i <> j && j <> k && i <> k

let ok2 i j k =
    let l = [ i; j; k ] |> List.sort
    List.min l = j || List.max l = j

let solve() =
    let s = read string
    let mutable mp = Dictionary<char, int>()
    mp.Add('E', 0)
    mp.Add('W', 0)
    mp.Add('S', 0)
    mp.Add('N', 0)
    for a in s do
        mp.[a] <- mp.[a] + 1

    let h = abs (mp.['N'] - mp.['S'])
    let w = abs (mp.['E'] - mp.['W'])
    let ans = float h ** 2. + float w ** 2. |> sqrt
    ans |> puts

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
