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
    let n = read int
    let mat = Array.zeroCreate n
    for i in 0 .. n - 1 do
        mat.[i] <- reada string

    let mutable ans = -1
    let mutable cnt = 0
    for j in 0 .. n - 1 do
        let mutable nyan = 0
        for i in 0 .. n - 1 do
            if i <> j && mat.[i].[j] = "nyanpass" then nyan <- nyan + 1
        if nyan = n - 1 then
            ans <- j + 1
            cnt <- cnt + 1

    let ans =
        if cnt = 1 then ans else -1
    ans |> puts
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
