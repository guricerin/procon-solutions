open System
open System.Collections.Generic

[<AutoOpen>]
module Cin =
    let read f = stdin.ReadLine() |> f
    let reada f = stdin.ReadLine().Split() |> Array.map f

    let readInts() =
        read string
        |> Seq.toArray
        |> Array.map (fun x -> Convert.ToInt32(x.ToString()))

module Util =
    let strRev s =
        s
        |> Seq.rev
        |> Seq.map string
        |> String.concat ""

[<EntryPoint>]
let main _ =
    let s = read string |> Seq.toArray
    let len = s |> Array.length
    let arr = ResizeArray<string>()

    let mutable l = 0
    while l < len do
        let mutable r = l + 1
        while r < len && 'a' <= s.[r] && s.[r] <= 'z' do
            r <- r + 1
        r <- r + 1
        r <- min r len
        let x = s.[l..r-1] |> String.Concat
        arr.Add(x)
        l <- r

    arr.Sort()
    let ans = arr |> String.Concat
    printfn "%s" ans
    0 // return an integer exit code
