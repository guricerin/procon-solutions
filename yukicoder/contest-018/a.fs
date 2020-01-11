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

let f (c: char) i =
    let i = i % 26
    let a = int c - int 'A'
    let a = a - (i + 1)

    let a =
        if a < 0 then a + 26 else a

    let a = a + int 'A'
    char a

let solve() =
    let s = readChars()
    let ans = s |> Array.mapi (fun i x -> f x i)
    ans
    |> fun a -> String.Join("", a)
    |> puts
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
