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

let toM h = h * 60

let limit = toM 24

let f (s: string array) =
    let hm1 = s
    let h1, m1 = Convert.ToInt32(hm1.[0]), Convert.ToInt32(hm1.[1])
    m1 + toM h1

let solve() =
    let n = read int
    let mutable ans = 0
    for i in 0 .. n - 1 do
        let s = reada string |> Array.map (fun x -> x.Split(':'))

        let hm1 = f s.[0]
        let hm2 = f s.[1]
        if hm1 <= hm2 then ans <- ans + (hm2 - hm1) else ans <- ans + (limit - hm1) + hm2

    puts ans
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
