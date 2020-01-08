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
    let ss = readChars()
    let len = Array.length ss
    let mutable ans = Int32.MaxValue
    for i in 0 .. len - 1 do
        if ss.[i] = 'c' then
            let mutable w = 0
            let mutable j = i + 1
            while w < 2 && j < len do
                if ss.[j] = 'w' then w <- w + 1
                j <- j + 1
            if w = 2 then ans <- min ans (j - i)

    let ans =
        if ans = Int32.MaxValue then -1 else ans
    ans |> puts
    ()

[<EntryPoint>]
let main _ =
    try
        solve()
    with e -> printfn "%s" (e.ToString())
    writer.Close()
    0 // return an integer exit code
