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
    let c1 = read string
    let c2 = read string
    let c = c1 + c2 |> Seq.toArray
    let mutable l = 0
    let lim = 14
    let mutable ans = 0
    while l < lim do
        let mutable r = l
        while r < lim && c.[r] = 'o' do
            r <- r + 1
        let tmp = r - l
        ans <- max ans tmp

        l <- r + 1
    ans |> puts
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
