open System

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
    let s = read string
    let len = s.Length
    let mid = len / 2

    let l, r =
        if len % 2 = 0 then s.Substring(0, mid), s.Substring(mid)
        else s.Substring(0, mid), s.Substring(mid + 1)

    let r = Util.strRev r

    Seq.map2 (fun x y ->
        if x <> y then 1
        else 0) l r
    |> Seq.sum
    |> printfn "%d"
    0 // return an integer exit code
