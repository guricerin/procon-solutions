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
    let S = readChars()
    let T = readChars()
    let tlen = T |> Array.length
    if tlen = 1 then
        for c in S do
            if c = T.[0] then
                printfn "-1"
                exit 0

    let slen = S |> Array.length
    let mutable ans = 0
    let mutable i = 0
    let ss = String.Join("", S)
    let t = String.Join("", T)
    while i + tlen <= slen do
        let s = ss.Substring(i, tlen)
        let ok = s = t
        if ok then ans <- ans + 1
        let k =
            if ok then tlen - 1 else 1
        i <- i + k
    puts ans
    ()

// -----------------------------------------------------------------------------------------------------
main()
writer.Dispose()
