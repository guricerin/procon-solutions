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
    let f (s: string) = s.Split('.')

    let a = read string |> f
    let b = read string |> f

    let rec loop i =
        if i > 2 then
            puts "YES"
        else
            let c, d = a.[i], b.[i]
            let c = Convert.ToInt32(c)
            let d = Convert.ToInt32(d)
            if c < d then puts "NO"
            else if c > d then puts "YES"
            else loop (i + 1)

    loop 0

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
